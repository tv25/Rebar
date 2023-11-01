using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Rebar.Models;

namespace Rebar.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IMongoCollection<Order> _OrderCollection;
        private readonly IOptions<DataBaseSettings> _dbSettings;
        private ShakeServices _shakeServices;
        public OrderServices(IOptions<DataBaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var MongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var MongoDatabase = MongoClient.GetDatabase(dbSettings.Value.DataBaseName);
            _OrderCollection = MongoDatabase.GetCollection<Order>
                (dbSettings.Value.OrderCollectionName);
        }
        public async Task<IEnumerable<Order>> GetAllOrders() =>
            await _OrderCollection.Find(_ => true).ToListAsync();


        public async Task<Order> GetOrderById(Guid id) =>
            await _OrderCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task AddOrder(Order order) =>
            await _OrderCollection.InsertOneAsync(order);

        public async Task UpdateOrder(Guid id, Order order) =>
            await _OrderCollection
            .ReplaceOneAsync(a => a.Id == id, order);

        public async Task<Shake> ManageMenu()
        {
            List<Order> orders = new List<Order>();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Choose a Shake");
            Console.WriteLine("2. Create a Shake");
            string choice = Console.ReadLine();
            if (!(int.TryParse(choice, out int choiceNumber)) || choiceNumber < 1 || choiceNumber > 2)
                throw new Exception("invalid input");
            if (choice == "1")
            {

                IEnumerable<Shake> availableShakes = await _shakeServices.GetAllShakes();
                if (availableShakes.Any())
                {
                    Console.WriteLine("Choose a shake:");
                    List<Shake> shakeList = availableShakes.ToList();

                    for (int i = 0; i < shakeList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {shakeList[i].Name} Shake");
                    }
                    string shakeChoiced = Console.ReadLine();

                    if (int.TryParse(shakeChoiced, out choiceNumber) && choiceNumber >= 1 && choiceNumber <= shakeList.Count)
                    {
                        Shake selectedShake = shakeList[choiceNumber - 1];
                        return selectedShake;
                        //Console.WriteLine($"You selected: {selectedShake.Name} Shake");
                    }
                    else
                    {
                        throw new Exception("invalid choice");
                    }
                }
            }
            Console.WriteLine("Enter the shake name:");
            string shakeName = Console.ReadLine();
            Console.WriteLine("Enter the shake description:");
            string shakeDescription = Console.ReadLine();
            Shake newShake = new Shake
            {
                Name = shakeName,
                Description = shakeDescription,
            };

            return newShake;

        }
        public async Task<List<Shake>> GetShakesInOrder()
        {
            int maxShakeSelections = 10;
            List<Shake> selectedShakes = new List<Shake>();

            for (int i = 0; i < maxShakeSelections; i++)
            {

                Shake selectedShake = await ManageMenu();
                if (selectedShake == null)
                {
                    throw new Exception("No shake was selected .");
                }
                selectedShakes.Add(selectedShake);

                Console.WriteLine("Do you want to choose another shake? (Y/N)");
                string continueChoice = Console.ReadLine().Trim();
                if (!continueChoice.Equals("Y", StringComparison.OrdinalIgnoreCase) || (!continueChoice.Equals("N", StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("No shake was selected .");
                }
                if (!continueChoice.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

            }
            return selectedShakes;
        }
        public async Task<Shakes> GetSizeFoShake(Shake shake)
        {
            Console.WriteLine("Choose a size:");

            foreach (Sizes size in Enum.GetValues(typeof(Sizes)))
            {
                int sizeValue = (int)size;
                string sizeString = size.ToString();
            }
            string userInput = Console.ReadLine();
            if (!(int.TryParse(userInput, out int choice)) || Enum.IsDefined(typeof(Sizes), choice))
            {
                throw new Exception("Invalid size.");
                
            }
            Sizes selectedSize = (Sizes)choice;
            int price = 0;

            switch (selectedSize)
            {
                case Sizes.S:
                    price = shake.S;
                    break;
                case Sizes.M:
                    price = shake.M;
                    break;
                case Sizes.L:
                    price = shake.L;
                    break;
            }
            Shakes myShakeRecord = new Shakes(shake, selectedSize, price);
            return myShakeRecord;
        }

        
        /*public async Task RedemptionOfDiscounts(Order order)
        {
            Console.WriteLine("Do you want to redeem a discount ? (Y/N)");
            string continueChoice = Console.ReadLine().Trim();
            if (!continueChoice.Equals("Y", StringComparison.OrdinalIgnoreCase) || (!continueChoice.Equals("N", StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("No shake was selected .");
            }
            if (!continueChoice.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            Console.WriteLine("Choose a Discount:");

            foreach (Discounts discounts in Enum.GetValues(typeof(Discounts)))
            {
                int discountsValue = (int)discounts;
                string discountsString = discounts.ToString();
            }
            string userInput = Console.ReadLine();
            if (!(int.TryParse(userInput, out int choice)) || Enum.IsDefined(typeof(Sizes), choice))
            {
                throw new Exception("Invalid discounts.");

            }
            Discounts selecteddiscounts = (Discounts)choice;

            switch (selecteddiscounts)
            {
                case Discounts.A:
                    order.Discounts.Add();
                    break;
                case Sizes.M:
                    price = shake.M;
                    break;
                case Sizes.L:
                    price = shake.L;
                    break;
            }
            Shakes myShakeRecord = new Shakes(shake, selectedSize, price);
            return myShakeRecord;



        }*/

        public async Task CreateOrder()
        {
            Order newOrder = new Order();
            newOrder.OrderDate = DateTime.Now;
            Console.WriteLine("Please enter your name:");
            string clientName = Console.ReadLine();
            newOrder.ClientName = clientName;
            List<Shake> AllShakeOrdered = await GetShakesInOrder();
            int priceOfOrder = 0;
            foreach (Shake shake in AllShakeOrdered)
            {
                try
                {
                    Shakes myShakeRecord = await GetSizeFoShake(shake);
                    newOrder.SelectedShakes.Add(myShakeRecord);
                    priceOfOrder += myShakeRecord.price;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            newOrder.TotalPrice = priceOfOrder;
            newOrder.OrderEndDate = DateTime.Now;
            await AddOrder(newOrder);

        }

    }
}


    



    

