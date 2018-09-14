using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class StoneApi
    {

        public class BoletoTransactionResult // not need implement or need? maybe, dont now :D
        { 

        }
       
        public class CreateSaleRequest
        {
            public CreditCardTransactionCollection CreditCardTransactionCollection { get; set; }
            public Order Order { get; set; }
        }
        public class CreateSaleResponse
        {
            public String ErrorReport { get; set; }
            public int InternalTime { get; set; }
            public String MerchantKey { get; set; }
            public String RequestKey { get; set; }
            //public BoletoTransactionResultCollection BoletoTransactionResultCollection { get; set; }
            public String BuyerKey { get; set; }
            public CreditCardTransactionResultCollection CreditCardTransactionResultCollection { get; set; }
            public OrderResult OrderResult { get; set; }
        }
        public class CreditCardTransactionResultCollection : List<CreditCardTransactionResult>
        {

        }
        public class CreditCardTransactionResult
        {
            public string AcquirerMessage { get; set; }
            public string AcquirerName { get; set; }
            public int AcquirerReturnCode { get; set; }
            public string AffiliationCode { get; set; }
            public int AmountInCents { get; set; }
            public string AuthorizationCode { get; set; }
            public int AuthorizedAmountInCents { get; set; }
            public int CapturedAmountInCents { get; set; }
            public DateTime CapturedDate { get; set; }
            public CreditCard CreditCard { get; set; }
            public string CreditCardOperation { get; set; }
            public string CreditCardTransactionStatus { get; set; }
            public DateTime? DueDate { get; set; }
            public int ExternalTime { get; set; }
            public string PaymentMethodName { get; set; }
            public int? RefundedAmountInCents { get; set; }
            public bool Success { get; set; }
            public string TransactionIdentifier { get; set; }
            public string TransactionKey { get; set; }
            public string TransactionKeyToAcquirer { get; set; }
            public string TransactionReference { get; set; }
            public string UniqueSequentialNumber { get; set; }
            public int? VoidedAmountInCents { get; set; }
        }
        public class Order
        {
            public string OrderReference { get; set; }
        }
        public class OrderResult
        {
            public DateTime CreateDate { get; set; }
            public string OrderKey { get; set; }
            public string OrderReference { get; set; }
        }
        public class CreditCard
        {
            // response
            public CreditCardBrandEnum CreditCardBrand { get; set; }
            public string InstantBuyKey { get; set; }
            public bool IsExpiredCreditCard { get; set; }
            public string MaskedCreditCardNumber { get; set; }

            //request
            public String CreditCardNumber { get; set; }
            public int ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string SecurityCode { get; set; }
            public String HolderName { get; set; }
        }
        public class CreditCardTransaction
        {

            public int AmountInCents { get; set; }

            public CreditCard CreditCard { get; set; }

            public int InstallmentCount { get; set; }


        }
        public class CreditCardTransactionCollection : List<CreditCardTransaction>
        {
            public CreditCardTransactionCollection(IEnumerable<CreditCardTransaction> collection) : base(collection)
            {

            }
        }

        public class Sale
        {
            public HttpResponse Create(CreateSaleRequest service)
            {
                // aqui voce trata os retornos
                return new HttpResponse();
            }
        }
        public class MockGatewayServiceClient
        {
            Guid merchantKey;
            public MockGatewayServiceClient(Guid merchantKey)
            {
                this.merchantKey = merchantKey;
                _sale = new Sale();
            }

            Sale _sale;
            public Sale Sale { get { return _sale; } }

        }

        public enum CreditCardBrandEnum
        {
            Visa,
            Mastercard,
            Elo
        }

        public class HttpResponse
        {
            public int HttpStatusCode { get; set; }
            public CreateSaleResponse Response { get; set; }
        }
        public class GatewayServiceClient
        {

        }
        public static void test()
        {
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 10000,
                CreditCard = new CreditCard()
                {
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    CreditCardNumber = "4111111111111111",
                    ExpMonth = 10,
                    ExpYear = 22,
                    HolderName = "LUKE SKYWALKER",
                    SecurityCode = "123"
                },
                InstallmentCount = 1
            };

            // Cria requisição.
            var createSaleRequest = new CreateSaleRequest()
            {
                // Adiciona a transação na requisição.
                CreditCardTransactionCollection = new CreditCardTransactionCollection(new CreditCardTransaction[] { transaction }),
                Order = new Order()
                {
                    OrderReference = "NumeroDoPedido"
                }
            };

            // Coloque a sua MerchantKey aqui.
            Guid merchantKey = Guid.Parse("F2A1F485-CFD4-49F5-8862-0EBC438AE923");

            // Cria o client que enviará a transação.
            var serviceClient = new MockGatewayServiceClient(merchantKey);

            // Autoriza a transação e recebe a resposta do gateway.
            var httpResponse = serviceClient.Sale.Create(createSaleRequest);

            Console.WriteLine("Código retorno: {0}", httpResponse.HttpStatusCode);
            Console.WriteLine("Chave do pedido: {0}", httpResponse.Response.OrderResult.OrderKey);
            if (httpResponse.Response.CreditCardTransactionResultCollection != null)
            {
                Console.WriteLine("Status transação: {0}", httpResponse.Response.CreditCardTransactionResultCollection.FirstOrDefault().CreditCardTransactionStatus);
            }
        }
    }
}
