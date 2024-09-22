using AutoMapper;
using CartAPI.Context;
using CartAPI.DTOs;
using CartAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        

        public async Task<bool> CleanCartAsync(string userId)
        {
            //encontra o header pelo userId
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            //verifica se não é null
            if(cartHeader is not null)
            {
                //produra no cartItems todos os itens do header
                _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));

                //deleta o header
                _context.CartHeaders.Remove(cartHeader);

                //salva e aplica ao db
                await _context.SaveChangesAsync();

                //retorna true
                return true;
            }
            
            return false;
        }       

        public async Task<bool> DeleteItemCartAsync(int cartItemId)
        {
            try
            {
                //encontra o item
                CartItem cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);

                //conta quantos itens ainda tem no carrinho
                int totalCount = _context.CartItems.Where(c => c.CartHeaderId == cartItemId).Count();

                //deleta o item
                _context.CartItems.Remove(cartItem);

                //se o totalcount é igual a um produto apenas, significa que o carrinho vai estar vazio e não faz sentido manter
                //o header, então fazemos uma verificação 
                if (totalCount == 1)
                {
                    //se true, procura no db o header
                    var cartHeaderRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);

                    //e deleta o header
                    _context.CartHeaders.Remove(cartHeaderRemove);
                }

                //salva
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                //ocorrendo algum erro
                return false;
            }
        }

        public async Task<CartDTO> GetCartByUserId(string userId)
        {
            try
            {
                Cart cart = new Cart
                {
                    //obter o header pelo userId
                    CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId)
                };

                //obter os itens
                cart.CartItems = _context.CartItems
                    //onde o cartHeader seja igual ao UserId
                    .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                    //exibimos os produtos
                    .Include(c => c.Product);
                
                //retorna mapeado
                return _mapper.Map<CartDTO>(cart);

            }
            catch (Exception)
            {
                //if something goes wrong
                return null;
            }
        }

        public async Task<CartDTO> UpdateCartAsync(CartDTO cartDTO)
        {
            var cart = _mapper.Map<Cart>(cartDTO);

            await SaveProductInDB(cartDTO, cart);

            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

            if(cartHeader is null)
            {
                await CreateCartHeaderAndItems(cart);
            }
            else
            {
                await UpdateQuantityAndItems(cartDTO, cart, cartHeader);
            }

            return _mapper.Map<CartDTO>(cart);
        }


        public Task<bool> DeleteCouponsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplyCouponsAsync(string userId, string couponId)
        {
            throw new NotImplementedException();
        }




        //PRIVATE METHODS TO HELP UPDATE CART
        //=========================================================================================================

        private async Task SaveProductInDB(CartDTO cartDTO, Cart cart)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartDTO.CartItems
                                                                      .FirstOrDefault().ProductId);

            if(product is null)
            {
                _context.Products.Add(cart.CartItems.FirstOrDefault().Product);
                
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateCartHeaderAndItems(Cart cart)
        {
            _context.CartHeaders.Add(cart.CartHeader);

            await _context.SaveChangesAsync();

            cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartItems.FirstOrDefault().Product = null;

            _context.CartItems.Add(cart.CartItems.FirstOrDefault());

            await _context.SaveChangesAsync();
        }

        private async Task UpdateQuantityAndItems(CartDTO cartDTO, Cart cart, CartHeader? cartHeader)
        {
            var cartDetail = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync
                    (p => p.ProductId == cartDTO.CartItems.FirstOrDefault().ProductId
                    && p.CartHeaderId == cartHeader.Id);

            if(cartDetail is null)
            {
                cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
                cart.CartItems.FirstOrDefault().Product = null;
                _context.CartItems.Add(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                cart.CartItems.FirstOrDefault().Product = null;
                cart.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
                cart.CartItems.FirstOrDefault().Id = cartDetail.Id;
                cart.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                _context.CartItems.Update(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();

            }
        }
    }
}
