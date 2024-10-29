﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.DTO.User
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressDelivery { get; set; }
    }
}
