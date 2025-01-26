class ProductPosition
{
    public string nameProductPosition { get; set; }
    public double baseCostProducrPosition { get; set; }

    public ProductPosition(string nameProductPosition, double baseCostProducrPosition) 
    {
        this.nameProductPosition = nameProductPosition;
        this.baseCostProducrPosition = baseCostProducrPosition;
    }

    public virtual string PrintAboutProduct() 
    {
        return $"Название: {nameProductPosition} - {baseCostProducrPosition}р.";
    }
}

class Menu
{
    public List<Drink> drinks { get; set; } = new List<Drink>();
    public List<TypesPizza> pizzas { get; set; } = new List<TypesPizza>();

    public List<Starter> starters { get; set; } = new List<Starter>();

    public List<Toppings> toppings { get; set; } = new List<Toppings>();

    public void AddInMenu(Drink drink)
    {
        drinks.Add(drink);
    }

    public void AddInMenu(TypesPizza typesPizza)
    {
        pizzas.Add(typesPizza);
    }
    public void AddInMenu(Starter starter)
    {
        starters.Add(starter);
    }
    public void AddInMenu(Toppings topping)
    {
        toppings.Add(topping);
    }

    public void PrintMenu()
    {
        Console.WriteLine("Для напитков и пицц доступно три размера. Маленький, средний, большой");
        Console.WriteLine("Напитки: ");
        Console.WriteLine();
        foreach (Drink drink in drinks)
        {
            Console.WriteLine(drink.PrintAboutProductMenu());
        }
        Console.WriteLine("_________________________");
        Console.WriteLine("Закузки: ");
        Console.WriteLine();
        foreach (Starter starter in starters)
        {
            Console.WriteLine(starter.PrintAboutProduct());
        }
        Console.WriteLine("_________________________");
        Console.WriteLine("Пиццы:");
        Console.WriteLine();
        foreach (TypesPizza pizza in pizzas)
        {
            pizza.PrintPizza();
        }
        Console.WriteLine();
    }
}

class Pizza 
{
    public TypesPizza typesPizza { get; set; }
    public Dough dough { get; set; }

    internal string sizePizza_str;
    public sizeOfPosition sizePizza { get; set; }

    public double fullPriceForPizza { get; private set; }
    List<Toppings> dopToppings { get; set; } = new List<Toppings> { };

   
    public Pizza(TypesPizza typesPizza)
    {
        this.typesPizza = typesPizza;

    }

    public void AddTopping(Toppings toppings)
    {
        dopToppings.Add(toppings);
        fullPriceForPizza += toppings.baseCostProducrPosition;

    }

    public double FullPizzaSumma()
    {
        if (sizePizza == sizeOfPosition.small)
            fullPriceForPizza += typesPizza.baseCostPizza * 0.5;
        else if (sizePizza == sizeOfPosition.medium)
            fullPriceForPizza += typesPizza.baseCostPizza * 1;
        else
            fullPriceForPizza += typesPizza.baseCostPizza * 1.5;

        return fullPriceForPizza;

    }
   public void DetailPizzza()
   {
        ConvertSize();
        dough.ConvertDought();
        Console.WriteLine($"Ваша пицца: {typesPizza.namePizza} - Размер: {sizePizza_str} - {fullPriceForPizza}р");
        Console.WriteLine($"\tТесто - {dough.thicknessDough_str}, {dough.typeDough_str}");
        if(dopToppings.Count != 0)
        {
            foreach(Toppings topping in dopToppings)
            {
                Console.WriteLine($"\t{topping.nameProductPosition} - Тип продукта: {topping.typeTopping_str}");
            }
        }

   }
    public void ConvertSize()
    {
        switch (sizePizza)
        {
            case sizeOfPosition.small:
                sizePizza_str = "Маленький";
                break;
            case sizeOfPosition.medium:
                sizePizza_str = "Средний";
                break;
            case sizeOfPosition.large:
                sizePizza_str = "Большой";
                break;
        }
    }

}


class Dough
{
    internal string thicknessDough_str;
    internal string typeDough_str;
    public thicknessDough thicknessDough { get; set; }

    public typeDough typeDough { get; set; }

    public void ConvertDought()
    {
        switch (thicknessDough)
        {
            case thicknessDough.Thin:
                thicknessDough_str = "Тонкое";
                break;
            case thicknessDough.Thick:
                thicknessDough_str = "Пышное";
                break;
        }
        switch (typeDough)
        {
            case typeDough.Normal:
                typeDough_str = "Обычное";
                break;
            case typeDough.Lactosefree:
                typeDough_str = "Безлактозное";
                break;
            case typeDough.Diet:
                typeDough_str = "Диетическое";
                break;
        }
    }


}
enum thicknessDough
{
    Thin,
    Thick
}
enum typeDough
{
    Normal,
    Lactosefree,
    Diet
}
enum sizeOfPosition
{
    small = 1,
    medium = 2,
    large = 3
}
enum typeTopping
{
    Meat,
    Lactose,
    Fruit,
    Vegetable,
    Fish
}

class TypesPizza
{
    public string namePizza {  get; set; }
    public double baseCostPizza { get; set; }
    public List <Toppings> toppings { get; set; } = new List<Toppings>();

    public TypesPizza(string namePizza, double baseCostPizza, List<Toppings> toppings) 
    {
        this.namePizza = namePizza;
        this.baseCostPizza = baseCostPizza;
        this.toppings = toppings;
    }

    public void ToppingPizza()
    {
        Console.WriteLine($"Состав пиццы {namePizza}:");
        foreach (Toppings topping in toppings)
        {
            Console.WriteLine($"\t{topping.nameProductPosition} - Тип продукта: {topping.typeTopping_str}");
        }
    }

    public void  PrintPizza()
    {
        Console.WriteLine($"Название: {namePizza} - {baseCostPizza}р.");
    }


}
class Toppings : ProductPosition
{
    internal string typeTopping_str;
    public typeTopping typeTopping
    {
        set
        {
            switch (value)
            {
                case typeTopping.Meat:
                    typeTopping_str = "Мясо";
                    break;
                case typeTopping.Fish:
                    typeTopping_str = "Рыба";
                    break;
                case typeTopping.Fruit:
                    typeTopping_str = "Фрукт";
                    break;
                case typeTopping.Vegetable:
                    typeTopping_str = "Овощ";
                    break;
                case typeTopping.Lactose:
                    typeTopping_str = "Содержит лактозу";
                    break;
            }

        }
        get { return typeTopping; }
    }

    public Toppings (string nameTopping, double baseCostTopping, typeTopping typeTopping) : base (nameTopping, baseCostTopping)
    {
        this.typeTopping = typeTopping;
    }
   

}
class Drink : ProductPosition
{
    internal string typeDrink_str;
    public double fullPriceForDrink { get; private set; }
    public sizeOfPosition sizeOfPosition {  get; set; }
 

  
    public Drink(string name, double cost) : base(name, cost)
    {
    }

    public double FullPriceDrink()
    {
        if (sizeOfPosition == sizeOfPosition.small)
            fullPriceForDrink += baseCostProducrPosition * 0.5;
        else if(sizeOfPosition == sizeOfPosition.medium)
            fullPriceForDrink += baseCostProducrPosition * 1;
        else
            fullPriceForDrink += baseCostProducrPosition * 1.5;

        return fullPriceForDrink;
    }
    public override string PrintAboutProduct()
    {
         ConvertSize();
         string fullDrink = "Напиток:\n " + $"Название: {nameProductPosition} - {fullPriceForDrink}р." + $"\tРазмер напитка - {typeDrink_str}";
         return fullDrink;
    }

    public string PrintAboutProductMenu()
    {
        string fullDrink = "Напиток:\n " + $"Название: {nameProductPosition} - {baseCostProducrPosition}р.";
        return fullDrink;
    }

    public void ConvertSize()
    {
        switch (sizeOfPosition)
        {
            case sizeOfPosition.small:
                typeDrink_str = "Маленький";
                break;
            case sizeOfPosition.medium:
                typeDrink_str = "Средний";
                break;
            case sizeOfPosition.large:
                typeDrink_str = "Большой";
                break;
        }
    }
}
class Starter : ProductPosition
{
    public Starter(string name, double cost) : base(name, cost) { }

    public override string PrintAboutProduct()
    {
        string fullStarter = "Закуска:\n" + base.PrintAboutProduct();
        return fullStarter;
    }


}
class Order
{
    public List<ProductPosition> orderedPositions { get; set; } = new List<ProductPosition>();
    public List<Pizza> orderedPizzas { get; set; } = new List<Pizza>();
    public int numberOrder {  get; private set; }
    public double totalPrice { get; set; } = 0;

    public User User { get; set; }

    public bool IsReady { get; set; } = false;

    Random random = new Random();

  

    public Order(User user)
    {
       
        numberOrder = random.Next(1000,9999);
        User = user;
    }



    public void AddInOrder(ProductPosition productPosition)
    {
        orderedPositions.Add(productPosition);
    }
    public void AddInOrder(Pizza pizza)
    {
        orderedPizzas.Add(pizza);
    }

    private double TotalPrice()
    {
        if (orderedPositions.Count != 0)
        {
         
            foreach (ProductPosition productPosition in orderedPositions)
            {
                if (productPosition is Drink)
                {
                    totalPrice += (productPosition as Drink).fullPriceForDrink;
                }
                else
                {
                    totalPrice += (productPosition as Starter).baseCostProducrPosition;
                }
            }
        }
        if(orderedPizzas.Count != 0)
        {
            foreach (Pizza pizza in orderedPizzas)
            {
                totalPrice += pizza.fullPriceForPizza;
            }
        }
       
        return totalPrice;
    }

    public void AllOrderPrint()
    {
        TotalPrice();
        Console.WriteLine($"Ваш заказ № {numberOrder} :");
        if(orderedPizzas.Count != 0)
        {
            foreach (Pizza pizza in orderedPizzas)
            {
                pizza.DetailPizzza();
                Console.WriteLine("------------------------------------------");
            }
        }

        if(orderedPositions.Count != 0)
        {
            foreach (ProductPosition productPosition in orderedPositions)
            {
                Console.WriteLine(productPosition.PrintAboutProduct());
                Console.WriteLine("------------------------------------------");
            }
        }
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"ИТОГОВАЯ ЦЕНА: {totalPrice}");
    }


}



class User
{
    public string Name { get; set; }
    public User(string name)
    {
        Name = name;
    }
}

class PizzaPlace
{
    public string Name { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();

    public event Action<Order> OnOrderReady;

    public PizzaPlace(string name)
    {
        Name = name;
    }
    public void PlaceOrder(Order order)
    {
        Orders.Add(order);
        // Эмуляция процесса приготовления (задержка)
        Thread.Sleep(new Random().Next(2000, 5000));

        order.IsReady = true;

        // Оповещение о готовности
        OnOrderReady?.Invoke(order);
    }

    public void DisplayOrderBoard()
    {
        Console.WriteLine($"\nТабло готовых заказов в {Name}:");
        foreach (var order in Orders.Where(o => o.IsReady))
        {
            Console.WriteLine($"- {order.User.Name}");
        }
    }


}
