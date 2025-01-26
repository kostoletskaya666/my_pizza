using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace my_pizza
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            PizzaPlace myPizzaPlace = new PizzaPlace("Ням-ням");
            myPizzaPlace.OnOrderReady += (order) =>
            {
                Console.WriteLine($"\nОповещение: Пицца для {order.User.Name} готова!");

            };



            Menu myMenu = new Menu();

            Drink product = new Drink("Кола", 200);
            myMenu.AddInMenu(product);
            Drink product_1 = new Drink("Вода", 100);
            myMenu.AddInMenu(product_1);
            Drink product_2 = new Drink("Сок", 250);
            myMenu.AddInMenu(product_2);
            Drink product_3 = new Drink("Кофе", 300);
            myMenu.AddInMenu(product_3);

            Starter product_s = new Starter("Сырные палочки", 200);
            myMenu.AddInMenu(product_s);
            Starter product_s1 = new Starter("Наггетсы", 100);
            myMenu.AddInMenu(product_s1);
            Starter product_s2 = new Starter("Салат Греческий", 250);
            myMenu.AddInMenu(product_s2);
            Starter product_s3 = new Starter("Салат Цезарь", 300);
            myMenu.AddInMenu(product_s3);

            Toppings toppings = new Toppings("Сыр Обычный", 30, typeTopping.Lactose);
            myMenu.AddInMenu(toppings);
            Toppings toppings_5 = new Toppings("Сыр Гауда", 30, typeTopping.Lactose);
            myMenu.AddInMenu(toppings_5);
            Toppings toppings_1 = new Toppings("Помидор", 20, typeTopping.Vegetable);
            myMenu.AddInMenu(toppings_1);
            Toppings toppings_2 = new Toppings("Перец", 20, typeTopping.Vegetable);
            myMenu.AddInMenu(toppings_2);
            Toppings toppings_3 = new Toppings("Пеперонни", 50, typeTopping.Meat);
            myMenu.AddInMenu(toppings_3);
            Toppings toppings_4 = new Toppings("Сосиски", 40, typeTopping.Meat);
            myMenu.AddInMenu(toppings_4);


            TypesPizza typesPizza_1 = new TypesPizza("Пеперони", 400, new List<Toppings> {toppings_3, toppings });
            myMenu.AddInMenu(typesPizza_1);
            TypesPizza typesPizza_2 = new TypesPizza("Маргарита", 450, new List<Toppings> { toppings_1, toppings });
            myMenu.AddInMenu(typesPizza_2);
            TypesPizza typesPizza_3 = new TypesPizza("Сырная", 400, new List<Toppings> { toppings_5, toppings });
            myMenu.AddInMenu(typesPizza_3);
            TypesPizza typesPizza_4 = new TypesPizza("Мясная", 550, new List<Toppings> { toppings_3, toppings_4 });
            myMenu.AddInMenu(typesPizza_4);

            StartPizza(myPizzaPlace, myMenu);
            Work(myPizzaPlace);
            Console.ReadKey();
        }

        public static void StartPizza(PizzaPlace pizzaPlace, Menu menu)
        {
            Console.WriteLine($"Добро пожаловать в {pizzaPlace.Name}");
            while (true)
            {
                Console.WriteLine("Выберите действие: 1 - посмотреть меню. 2 - сделать заказа. 3 - выйти.");
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    switch (x)
                    {
                        case 1:
                            menu.PrintMenu();
                            continue;
                        case 2:
                            MakeOrder(pizzaPlace, menu);
                            break;
                        case 3:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                }
                break;
            }
        }

        public static void Work(PizzaPlace pizzaPlace)
        {
            // Ждем пока все заказы будут готовы
            Thread.Sleep(6000);

            // Показываем табло
            pizzaPlace.DisplayOrderBoard();
        }
        public static void MakeOrder(PizzaPlace pizzaPlace, Menu menu)
        {
            Console.WriteLine("Ваше имя:");
            string name = Console.ReadLine();
            User user = new User(name);

            Order order =  new Order(user);
            GiveInOrder(pizzaPlace, menu, order);
        }

        public static void GiveInOrder(PizzaPlace pizzaPlace, Menu menu, Order order)
        {
            while (true)
            {
                Console.WriteLine("Что хотите заказать? 1. Пиццу 2. Напитки 3. Закузки 4. Посмотреть заказ  5. Завершить заказ 6. Выход в главное меню");
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    switch (x)
                    {
                        case 1:
                            GivePizza(pizzaPlace, menu, order);
                            continue;
                        case 2:
                            GiveDrink(pizzaPlace, menu, order);
                            continue;
                        case 3:
                            GiveStarter(pizzaPlace, menu, order);
                            continue;
                        case 4:
                            order.AllOrderPrint();
                            continue;
                        case 5:
                            FinalOrder(pizzaPlace, menu, order);
                            break;
                        case 6:
                            StartPizza(pizzaPlace, menu);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    continue;
                }
                break;
            }

        }



        public static void FinalOrder(PizzaPlace pizzaPlace, Menu menu, Order order)
        {
            order.AllOrderPrint();
            Console.WriteLine($"Размещаем заказ {order.User.Name}");
            Console.WriteLine("Спасибо за заказ. Ожидайте");
            pizzaPlace.PlaceOrder(order);


        }


        public static void GivePizza(PizzaPlace pizzaPlace, Menu menu, Order order)
        {
            for (int i = 0; i < menu.pizzas.Count; i++)
            {

                Console.WriteLine(Convert.ToString(i + 1) + " - " );
                menu.pizzas[i].PrintPizza();
                menu.pizzas[i].ToppingPizza();
                Console.WriteLine();

            }
            Console.WriteLine("Чтобы добавить в заказ напишите номер позиции. Если хотите вернуться назад в главное меню напишите 0.");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    if (x != 0 && x > 0 && x <= menu.pizzas.Count)
                    {
                        for (int i = 0; i < menu.pizzas.Count; i++)
                        {

                            if ((x - 1) == i)
                            {
                                Pizza pizza = new Pizza(menu.pizzas[i]);
                                while (true)
                                {
                                   
                                    //добавить топпинг
                                    Console.WriteLine("Хотите добавить дополнительные начинки? 1.Да 2.Нет");
                                    try 
                                    {
                                        int y = Convert.ToInt32(Console.ReadLine());
                                        if(y == 1)
                                        {
                                            GiveTopping(pizza, menu);
                                            break;
                                        }
                                        else if(y == 2)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }

                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine();
                                        continue;
                                    }
                                }

                                pizza.sizePizza = (sizeOfPosition)ChooseASize();
                                Dough dough = new Dough();
                                dough.typeDough = ChooseADoughtType();
                                dough.thicknessDough = ChooseADoughtThinkness();
                                pizza.dough = dough;
                                pizza.FullPizzaSumma();
                                order.AddInOrder(pizza);
                                Console.WriteLine("Вы добавили:");
                                pizza.DetailPizzza();
                                Console.WriteLine("Хотите продолжить? Если нет, то напишите 0");

                            }

                        }
                    }
                    else if (x == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не то");
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    continue;
                }
            }

        }
        public static void GiveTopping(Pizza pizza, Menu menu)
        {
            Console.WriteLine("Список начинок: ");
            for (int i = 0; i < menu.toppings.Count; i++)
            {

                Console.WriteLine(Convert.ToString(i + 1) + " - " + menu.toppings[i].PrintAboutProduct());
            }
            Console.WriteLine("Чтобы добавить в заказ напишите номер позиции. Если хотите вернуться назад в главное меню напишите 0.");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    if (x != 0 && x > 0 && x <= menu.toppings.Count)
                    {
                        for (int i = 0; i < menu.toppings.Count; i++)
                        {
                            if ((x - 1) == i)
                            {

                                pizza.AddTopping(menu.toppings[i]);
                                Console.WriteLine("Вы добавили:\n" + menu.toppings[i].PrintAboutProduct());
                                Console.WriteLine("Хотите продолжить? Если нет, то напишите 0");

                            }

                        }
                    }
                    else if (x == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не то");
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    continue;
                }
            }
        }
        public static void GiveDrink(PizzaPlace pizzaPlace, Menu menu, Order order)
            {
            for (int i = 0; i < menu.drinks.Count; i++)
            {
                
                    Console.WriteLine(Convert.ToString(i + 1) + " - " + menu.drinks[i].PrintAboutProductMenu());
                
            }
            Console.WriteLine("Чтобы добавить в заказ напишите номер позиции. Если хотите вернуться назад в главное меню напишите 0.");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    if (x != 0 && x > 0 && x <= menu.drinks.Count)
                    {
                        for (int i = 0; i < menu.drinks.Count; i++)
                        {
                            if ((x - 1) == i)
                            {
                                
                                menu.drinks[i].sizeOfPosition = (sizeOfPosition)ChooseASize();
                                menu.drinks[i].FullPriceDrink();
                                order.AddInOrder(menu.drinks[i]);
                                Console.WriteLine("Вы добавили:\n" + menu.drinks[i].PrintAboutProduct());
                                Console.WriteLine("Хотите продолжить? Если нет, то напишите 0");
                           
                            }

                        }
                    }
                    else if(x == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не то");
                        continue;
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Вы ввели не то");
                    continue;
                }
            }



        }
        public static void GiveStarter(PizzaPlace pizzaPlace, Menu menu, Order order)
        {
            for (int i = 0; i < menu.starters.Count; i++)
            {

                Console.WriteLine(Convert.ToString(i + 1) + " - " + menu.starters[i].PrintAboutProduct());

            }
            Console.WriteLine("Чтобы добавить в заказ напишите номер позиции. Если хотите вернуться назад в главное меню напишите 0.");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    if (x != 0 && x > 0 && x <= menu.starters.Count)
                    {
                        for (int i = 0; i < menu.starters.Count; i++)
                        {
                            if ((x - 1) == i)
                            {

                                order.AddInOrder(menu.starters[i]);
                                Console.WriteLine("Вы добавили:\n" + menu.starters[i].PrintAboutProduct());
                                Console.WriteLine("Хотите продолжить? Если нет, то напишите 0");

                            }

                        }
                    }
                    else if (x == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не то");
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    continue;
                }
            }



        }
        public static int ChooseASize()
        {
            Console.WriteLine("Выберите размер: 1. Маленький 2. Средний 3. Большой");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32((Console.ReadLine()));
                    if (x > 0 && x < 4)
                    {
                        if(x == 1)
                        {
                            return x;
                        }
                        else if (x == 2)
                        {
                            return x;
                        }
                        else
                        {
                            return x;
                        }
                        
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine();
                    continue;
                }
            }
        }
        public static typeDough ChooseADoughtType()
        {
            Console.WriteLine("Выберите тип теста: 1. Обычное 2. Диетическое 3. Безлактозное");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32((Console.ReadLine()));
                    if (x > 0 && x < 4)
                    {
                        if (x == 1)
                        {
                            return typeDough.Normal;
                        }
                        else if (x == 2)
                        {
                            return typeDough.Diet;
                        }
                        else
                        {
                            return typeDough.Lactosefree;
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    continue;
                }
            }
        }
        public static thicknessDough ChooseADoughtThinkness()
        {
            Console.WriteLine("Выберите тип теста: 1. Тонкое 2. Пышное ");
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32((Console.ReadLine()));
                    if (x > 0 && x < 3)
                    {
                        if (x == 1)
                        {
                            return thicknessDough.Thin;
                        }
                       
                        else
                        {
                            return thicknessDough.Thick;
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    continue;
                }
            }
        }
    }

    
}
