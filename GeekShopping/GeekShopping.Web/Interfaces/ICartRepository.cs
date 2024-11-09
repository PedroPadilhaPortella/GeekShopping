﻿using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Interfaces
{
    public interface ICartRepository
    {
        Task<CartDTO> GetCartByUserId(string userId);
        Task<CartDTO> SaveOrUpdateCart(CartDTO cartDTO);
        Task<bool> RemoveCartItem(long cartDetailId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}