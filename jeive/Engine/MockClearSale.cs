﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace jeive.Engine
{
    //https://www2.clear.sale/developers/api#appendix-requestsend

    class MockClearSale
    {
        public class ClearSaleOrder
        {
            IClearSaleOrderService service;

            public ClearSaleOrder(IClearSaleOrderService service)
            {
                this.service = service;
            }

            public ResponseSend Send(RequestSend auth)
            {
                return this.service.Send(auth);
            }

            public static void test()
            {
                MockClearSaleOrderService service = new MockClearSaleOrderService();
                ClearSaleOrder clearSaleOrder = new ClearSaleOrder(service);

                List<Order> orders = new List<Order>();
                List<Payment> payments = new List<Payment>();
                List<Item> items = new List<Item>();

                items.Add(new Item()
                {
                    ID = "SKU_ID1",
                    Name = "Item 1",
                    Qty = 1,
                    ItemValue = 10.02m
                });

                items.Add(new Item()
                {
                    ID = "SKU_ID2",
                    Name = "Item 2",
                    Qty = 1,
                    ItemValue = 20.02m
                });

                items.Add(new Item()
                {
                    ID = "SKU_ID3",
                    Name = "Item 3",
                    Qty = 1,
                    ItemValue = 30.02m
                });

                orders.Add(new Order()
                {
                    ID = "PDD_01",
                    Date = DateTime.Now,
                    Email = "customer@shop.com",
                    TotalItems = items.Count,
                    TotalOrder = items.Sum(i => i.ItemValue),
                    TotalShipping = 30,
                    IP = "127.0.0.1",
                    Currency = "BRL",
                    Payments = payments.ToArray(),
                    Items = items.ToArray(),
                    BillingData = new Person()
                    {

                    }

                });


                orders.Add(new Order()
                {
                    ID = "PDD_02",
                    Date = DateTime.Now,
                    Email = "customer@shop.com",
                    TotalItems = items.Count,
                    TotalOrder = items.Sum(i => i.ItemValue),
                    TotalShipping = 30,
                    IP = "127.0.0.1",
                    Currency = "USD",
                    Payments = payments.ToArray(),
                    Items = items.ToArray(),
                    BillingData = new Person()
                    {

                    }

                });

                ResponseSend result = clearSaleOrder.Send(new RequestSend()
                {
                    AnalysisLocation = "BRA",
                    ApiKey = "API_KEY",
                    LoginToken = "LOGIN_TOKEN",
                    Orders = orders.ToArray()
                });
                int f = 1;
            }
        }

        public interface IClearSaleOrderService
        {
            ResponseSend Send(RequestSend auth);
        }

        public class MockClearSaleOrderService : IClearSaleOrderService
        {

            public ResponseSend Send(RequestSend auth)
            {


                ResponseSend response = new ResponseSend();
                response.TransactionID = auth.AnalysisLocation;

                List<OrderStatus> orders = new List<OrderStatus>();

                foreach (Order order in auth.Orders)
                {
                    // assume pagamentos em dolar como fraude
                    if (order.Currency == "USD")
                    {
                        orders.Add(new OrderStatus()
                        {
                            ID = order.ID,
                            Score = 100.00m,
                            Status = "FRD"
                        });
                    }
                    else
                    {
                        orders.Add(new OrderStatus()
                        {
                            ID = order.ID,
                            Score = 50.00m,
                            Status = "APA"
                        });
                    }

                }
                response.Orders = orders.ToArray();

                return response;
            }
        }
        public class RequestSend
        {

            public String ApiKey { get; set; }
            public String LoginToken { get; set; }
            public Order[] Orders { get; set; }
            public String AnalysisLocation { get; set; }

        }

        public class ResponseSend
        {
            public OrderStatus[] Orders { get; set; }
            public String TransactionID { get; set; }
        }

        public class OrderStatus
        {
            public String ID { get; set; }
            public String Status { get; set; }
            public Decimal Score { get; set; }
        }
        public class Order
        {
            public String ID { get; set; }
            public DateTime Date { get; set; }
            public String Email { get; set; }
            public Decimal TotalShipping { get; set; }
            public Decimal TotalItems { get; set; }
            public Decimal TotalOrder { get; set; }
            public String IP { get; set; }
            public String Obs { get; set; }
            public String Currency { get; set; }
            public String Status { get; set; }
            public Payment[] Payments { get; set; }
            public Person ShippingData { get; set; }
            public Person BillingData { get; set; }
            public Item[] Items { get; set; }
            public CustomFields[] CustomFields { get; set; }
            public bool Reanalysis { get; set; }
            public String Origin { get; set; }
        }
        public class Payment
        {
            public DateTime Date { get; set; }
            public Decimal Amount { get; set; }
            public int Type { get; set; }
            public int QtyInstallments { get; set; }
            public String CardNumber { get; set; }
            public String CardBin { get; set; }
            public String CardEndNumber { get; set; }
            public int CardType { get; set; }
            public String CardExpirationDate { get; set; }
            public String CardHolderName { get; set; }
            public String Address { get; set; }
            public String Nsu { get; set; }
            public int Currency { get; set; }
        }
        public class Person
        {
            public String ID { get; set; }
            public int Type { get; set; }
            public String Name { get; set; }
            public DateTime BirthDate { get; set; }
            public String Email { get; set; }
            public String Gender { get; set; }
            public Address Address { get; set; }
            public Phone[] Phones { get; set; }
        }

        public class Address
        {
            public String AddressLine1 { get; set; }
            public String AddressLine2 { get; set; }
            public String City { get; set; }
            public String State { get; set; }
            public String Country { get; set; }
            public String ZipCode { get; set; }
        }
        public class Phone
        {
            public int Type { get; set; }
            public int CountryCode { get; set; }
            public int AreaCode { get; set; }
            public String Number { get; set; }
        }
        public class Item
        {
            public String ID { get; set; }
            public String Name { get; set; }
            public Decimal ItemValue { get; set; }
            public int Qty { get; set; }
            public int Gift { get; set; }
            public int CategoryID { get; set; }
            public String CategoryName { get; set; }
        }
        public class CustomFields
        {
            public int Type { get; set; }
            public DateTime Name { get; set; }
            public String Value { get; set; }
        }
    }
}