﻿using ParkingManager.DTO.MpesaC2BModule;
using ParkingManager.DTO.MpesaStkModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManager.BLL.Repositories.MpesaStkModule
{
    public interface IPaymentRepository
    {
        Task<CheckoutRequestDTO> SaveCheckoutRequest(CheckoutRequestDTO checkoutRequestDTO);
        Task<List<MpesaPaymentDTO>> GetAll();
        Task<MpesaPaymentDTO> GetByTransNumber(string TransactionId);
        CheckoutRequestDTO GetCheckOutRequestById(string CheckoutRequestID);
        void SaveSTKCallBackResponse(MpesaPaymentDTO mpesaPaymentDTO);
        bool SavePayBillCallBackResponse(MpesaPaymentDTO mpesaPaymentDTO);
        string GetTotalRevenue();
        bool IsTransactionExists(string TransactionNumber);
        bool ValidatePayment(string TransactionNumber);
        Task<MpesaPaymentDTO> CreateMpesaPayment(MpesaPaymentDTO mpesaPaymentDTO);
    }
}