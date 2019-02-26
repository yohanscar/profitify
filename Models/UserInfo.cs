using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profitify
{
    public class Balance
    {
        public string coin { get; set; }
        public decimal amount { get; set; }
        public decimal amountInOrder { get; set; }
        public decimal amountWithdrawl { get; set; }
        public decimal amountDeposit { get; set; }
        public decimal percent { get; set; }
    }

    public class Bank
    {
        public int idBank { get; set; }
        public int idBankList { get; set; }
        public string bankName { get; set; }
        public string febrabanBankCode { get; set; }
        public string agencia { get; set; }
        public string contaCorrente { get; set; }
        public decimal depositTax { get; set; }
        public decimal depositFixedTax { get; set; }
        public decimal withdrawlTax { get; set; }
        public decimal withdrawlFixedTax { get; set; }
        public object cpf { get; set; }
        public object cnpj { get; set; }
        public string owner { get; set; }
        public decimal amount { get; set; }
        public decimal amountMininum { get; set; }
        public decimal amountMaximum { get; set; }
    }

    public class UserInfo
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string nickName { get; set; }
        public string email { get; set; }
        public object cpf { get; set; }
        public object phone { get; set; }
        public object birth { get; set; }
        public bool t2FAEnabled { get; set; }
        public string status { get; set; }
        public DateTime lastLogin { get; set; }
        public bool thirdPartLogin { get; set; }
        public string feeLevel { get; set; }
        public decimal makerFee { get; set; }
        public decimal takerFee { get; set; }
        public decimal withdrawlFee { get; set; }
        public DateTime dateRegister { get; set; }
        public string appId { get; set; }
        public string apiKey { get; set; }
        public bool telegramEnabled { get; set; }
        public object telegramUserName { get; set; }
        public int telegramUserId { get; set; }
        public List<Balance> balances { get; set; }
        public List<Bank> banks { get; set; }
    }
}