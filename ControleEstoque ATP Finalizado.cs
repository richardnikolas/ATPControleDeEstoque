using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;

namespace SegundoTrabalhoPratico { 
    class Program {

        public class Product {
            public string name;
            public float unitPrice;
            public float totalProfit = 0f;
            public int quantityInStock;
            public int minimumQuantityForStock;
            public int totalOfProductsSold = 0;
        }

        static void Main (string[] args) {
            int theChoice;
            bool wannaLeave = false;
            List<Product> products = new List<Product>();

            ForegroundColor = ConsoleColor.White;

            RegisterProducts(products);

            theChoice = MainMenu(products);

            while (!wannaLeave) {
                if (theChoice != 0)
                    theChoice = MainMenu(products);
                else if (theChoice == 0)
                    wannaLeave = true;
            }
        }

        /*
         *   0) Invoca o Menu Principal
         */ 
        static int MainMenu (List<Product> products) {
            Clear();
            int choice = 99;
            bool answer = false, rightChoice;

            while (!answer) {
                BackgroundColor = ConsoleColor.DarkGreen;
                ForegroundColor = ConsoleColor.White;
                WriteLine("\n\t\t\t\t\t\t\t\t");
                WriteLine("\t $$$$ BEM VINDX AO MERCADINHO DELÍCIA $$$$\t\t");
                WriteLine("\t\t\t\t\t\t\t\t\n\n");
                ResetColor();

                ForegroundColor = ConsoleColor.White;
                WriteLine(" Temos as seguintes opções para você, queridx gerente:\n ");

                WriteLine("\t 1. Realizar uma venda");
                WriteLine("\t 2. Relatório geral do Mercadinho Delícia");
                WriteLine("\t 3. Imprimir ordenado pela quantidade no estoque");
                WriteLine("\t 4. Atualizar informações de um produto");
                WriteLine("\t 5. Hino da nossa linda loja");
                WriteLine("\t 0. Sair do programa");

                Write("\n Escolha uma opção (digite o numero referente a opção): ");
                rightChoice = int.TryParse(ReadLine(), out choice);

                if (!rightChoice) choice = 99;

                ResetColor();
            
                switch (choice) {
                    case 1:
                        SellProduct(products);
                        answer = true;
                        break;
                    case 2:
                        GenerateReport(products);
                        answer = true;
                        break;
                    case 3:
                        SortLargerStock(products);
                        answer = true;
                        break;
                    case 4:
                        UpdateProduct(products);
                        answer = true;
                        break;
                    case 5:
                        BestAnthemEver();
                        answer = true;
                        break;
                    case 0:
                        answer = true;
                        break;
                    default:
                        answer = false;
                        Clear();
                        BackgroundColor = ConsoleColor.Red;
                        ForegroundColor = ConsoleColor.White;         
                        WriteLine("\n\t Opção inválida! Tente novamente \t");
                        ResetColor();
                        Write("\n");                        
                        break;          
                }
            }

            return choice;
        }        

        /*
         *   1) Lê o nome do produto, sua quantidade em estoque, sua quantidade mínima 
         *      para o estoque e o valor unitário.
         */
        static void RegisterProducts (List<Product> products) {
            string name;
            int quantityStock, minimumQuantity;
            float price;

            ForegroundColor = ConsoleColor.White;

            WriteLine("\n Insira os 10 produtos do mercadinho: ");

            for (var i = 0; i < 10; i++) {
                Write("\n Nome do produto: ");
                name = ReadLine();

                Write(" Quantidade em estoque: ");
                quantityStock = int.Parse(ReadLine());

                Write(" Quantidade mínima para estoque: ");
                minimumQuantity = int.Parse(ReadLine());

                Write(" Preço unitário: ");
                price = (float) Convert.ToDouble(ReadLine());

                Product newProduct = new Product();
                newProduct.name = name;
                newProduct.quantityInStock = quantityStock;
                newProduct.minimumQuantityForStock = minimumQuantity;
                newProduct.unitPrice = price;

                products.Add(newProduct);
            }

            Clear();
        }

        /*
         *   2) Atualize a quantidade em estoque, quando um produto for vendido. A quantidade
         *      também deve ser armazenada. Além disso, a quantidade em estoque é atualizada.
         */
        static void SellProduct (List<Product> products) {
            int productNumber = 0, quantity;
            float totalPrice;
            bool productExists = false, quantityExists = false;

            Clear();
            ForegroundColor = ConsoleColor.White;

            WriteLine("\n Escolha qual produto deseja vender (de 1 a 10):\n ");
            for (var i = 0; i < products.Count; i++) 
                WriteLine(" [ " + (i+1) + " ] - " + products[i].name);

            while (!productExists) { 
                Write("\n Digite aqui o número do produto: ");
                productNumber = int.Parse(ReadLine()) - 1;

                if (productNumber >= 0 && productNumber < products.Count)
                    productExists = true;
            }

            while (!quantityExists) {
                Write(" Agora digite a quantidade de produtos (" + products[productNumber].name + ") vendidos: ");
                quantity = int.Parse(ReadLine());

                if (quantity > 0) {

                    if (quantity > products[productNumber].quantityInStock) { 
                        WriteLine("\n\t Não temos essa quantidade no nosso estoque! =( ");
                        WriteLine("\t O nosso total em estoque é : " + products[productNumber].quantityInStock);
                        WriteLine("\t Por favor, escolha uma quantidade menor do que isso. Obrigado.\n");
                    }
                        
                    else { 
                        totalPrice = (float) products[productNumber].unitPrice * quantity;

                        WriteLine("\n\n\t     RESUMO DA VENDA: ");
                        WriteLine("\t - Produto: \t" + products[productNumber].name);
                        WriteLine("\t - Quantidade: \t" + quantity);
                        Write("\t - Valor total: ");
                        BackgroundColor = ConsoleColor.DarkGreen;
                        Write(" R$" + totalPrice + " ");
                        ResetColor();

                        ForegroundColor = ConsoleColor.White;

                        products[productNumber].quantityInStock -= quantity;
                        products[productNumber].totalOfProductsSold += quantity;
                        products[productNumber].totalProfit += totalPrice;

                        quantityExists = true;
                    }
                }

                else if (quantity == 0)
                    quantityExists = true;
            }

            WriteLine("\n\n\n\n Aperte ENTER para voltar para o Menu Principal! ");
            ReadKey();
        }

        /*
         *   4) Gera um relatório contendo um balanço de todo o estoque 
         *      e das vendas de cada produto
         */
        static void GenerateReport (List<Product> products) {
            float profitUntilNow = 0f;
            Clear();

            ForegroundColor = ConsoleColor.White;
            Write("\n Até o momento, este é o nosso relatório geral: \n");

            foreach (var item in products) {

                profitUntilNow += item.totalProfit;

                Write("\n");
                WriteLine(" ________________________________________");
                if (item.name.Length > 15)
                    WriteLine(" | Nome: " + item.name + "\t\t |");
                else if (item.name.Length > 6)
                    WriteLine(" | Nome: " + item.name + "\t\t\t |");
                else
                    WriteLine(" | Nome: " + item.name + "\t\t\t\t |");

                WriteLine(" | Quantidade em estoque: " + item.quantityInStock + "\t\t |");

                if (item.minimumQuantityForStock >= 10)
                    WriteLine(" | Quantidade mínima: " + item.minimumQuantityForStock + "\t\t |");
                else
                    WriteLine(" | Quantidade mínima: " + item.minimumQuantityForStock + "\t\t\t |");

                if (item.quantityInStock >= item.minimumQuantityForStock) {
                    Write(" |");
                    ForegroundColor = ConsoleColor.Green;
                    Write(" > Estoque normal!");
                    ResetColor();
                    ForegroundColor = ConsoleColor.White;
                    WriteLine(" \t\t\t |");
                }
                else {
                    Write(" |");
                    ForegroundColor = ConsoleColor.Red;
                    Write(" > Estoque abaixo do mínimo!");
                    ResetColor();
                    ForegroundColor = ConsoleColor.White;
                    WriteLine(" \t\t |");
                }

                WriteLine(" | Total vendido: " + item.totalOfProductsSold + "\t\t\t |");

                if(item.totalProfit >= 100)
                    Write(" | Total de lucro: R$" + item.totalProfit + "\t\t |\n");
                else
                    Write(" | Total de lucro: R$" + item.totalProfit + "\t\t\t |\n");

                Write(" ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
            }

            Write("\n\n\t ~ LUCRO TOTAL ATÉ O MOMENTO: ");
            BackgroundColor = ConsoleColor.DarkGreen;
            Write(" R$" + profitUntilNow + " ");
            ResetColor();

            ForegroundColor = ConsoleColor.White;

            WriteLine("\n\n\n\n Aperte ENTER para voltar para o Menu Principal! ");
            ReadKey();
        }

        /*
         *   z) Função extra! Essa função atualizará algum produto da lista! 
         *      Por exemplo: aumentar/mudar a quantidade em estoque, aumentar/mudar o preço unitário
         */ 
        static void UpdateProduct (List<Product> products) {
            int productNumber = 0, quantityStock, minimumQuantity;
            float price;
            bool productExists = false, result;

            Clear();
            ForegroundColor = ConsoleColor.White;

            WriteLine("\n Escolha qual produto deseja atualizar (de 1 a 10):\n ");
            for (var i = 0; i < products.Count; i++)
                WriteLine(" [ " + (i + 1) + " ] - " + products[i].name);

            while (!productExists) { 
                Write("\n Digite aqui o número do produto: ");
                productNumber = int.Parse(ReadLine()) - 1;

                if (productNumber >= 0 && productNumber <= 10)
                    productExists = true;
            }

            var item = products[productNumber];

            WriteLine("\n Aqui agora você irá atualizar todas as informações sobre o produto (" + item.name + ") : ");
            WriteLine("\n ** Você pode simplesmente apertar ENTER para MANTER as informações atuais do produto **");

            Write("\n\t Quantidade em estoque (" + item.quantityInStock + ") : ");
            result = int.TryParse(ReadLine(), out quantityStock);
            if (result)
                products[productNumber].quantityInStock = quantityStock;

            Write("\t Quantidade mínima para estoque (" + item.minimumQuantityForStock + ") : ");
            result = int.TryParse(ReadLine(), out minimumQuantity);
            if (result)
                products[productNumber].minimumQuantityForStock = minimumQuantity;

            Write("\t Preço unitário (" + item.unitPrice + ") : ");
            result = float.TryParse(ReadLine(), out price);
            if (result)
                products[productNumber].unitPrice = price;

            Write("\n"); Write("\t ");
            BackgroundColor = ConsoleColor.DarkCyan;
            WriteLine(" SALVO COM SUCESSO! ");
            ResetColor();

            ForegroundColor = ConsoleColor.White;
            
            WriteLine("\n\n\n Aperte ENTER para voltar para o Menu Principal! ");
            ReadKey();
        }

        /*
         *   6) *Ponto Extra* Classificar e imprimir os nomes dos produtos por ordem de maior
         *      quantidade em estoque. 
         */
        static void SortLargerStock (List<Product> products) {
            Clear();

            int[] sortedQuantities;
            sortedQuantities = new int[10];

            ForegroundColor = ConsoleColor.White;

            WriteLine("\n Items ordenados por MAIOR quantidade em estoque: ");

            // List<Product> sortedList = products.OrderByDescending(item => item.quantityInStock).ToList();

            //foreach (var item in sortedList) {
            //    WriteLine("\n Item: " + item.name);
            //    WriteLine(" Em estoque: " + item.quantityInStock);
            //}

            for (var j = 0; j < products.Count; j++)
                sortedQuantities[j] = products[j].quantityInStock;

            QuickSort(sortedQuantities, 0, sortedQuantities.Length - 1);

            Array.Reverse(sortedQuantities);

            for (var k = 0; k < sortedQuantities.Length; k++) { 
                foreach (var item in products) {
                    if (item.quantityInStock == sortedQuantities[k]) {
                        WriteLine("\n\t Item: " + item.name);
                        WriteLine("\t Em estoque: " + item.quantityInStock);
                    }
                }
            }

            WriteLine("\n\n\n\n\n Aperte ENTER para voltar para o Menu Principal! ");
            ReadKey();
        }

        /*
         *    Quick Sort code 
         */ 
        public static void QuickSort(int[] A, int left, int right) {
            if (left > right || left < 0 || right < 0) return;

            int index = Partition(A, left, right);

            if (index != -1) {
                QuickSort(A, left, index - 1);
                QuickSort(A, index + 1, right);
            }
        }

        private static int Partition(int[] A, int left, int right) {
            if (left > right) return -1;

            int end = left;

            int pivot = A[right]; // choose last one to pivot, easy to code

            for (int i = left; i < right; i++) {
                if (A[i] < pivot) {
                    Swap(A, i, end);
                    end++;
                }
            }

            Swap(A, end, right);

            return end;
        }

        private static void Swap(int[] A, int left, int right) {
            int tmp = A[left];
            A[left] = A[right];
            A[right] = tmp;
        }

        /*
         *   x) Hino do Mercadinho Delícia  
         */
        static void BestAnthemEver () {
            Clear();

            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\n\t ** HINO DO MERCADINHO **");

            WriteLine("\n");
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.Cyan;

            WriteLine("\t\t\t\t\t\t\t\t");
            Write("\t No Mercadinho Delícia, sempre tem promoção \t\t\n");
            Write("\t Vem pagar barato na bebida e sacolão \t\t\t\n");
            Write("\t Não importa onde esteja, vem comprar com a gente \t\n");
            Write("\t Recomende pros amigos e, é claro, volte sempre <3 \t\n");
            WriteLine("\t\t\t\t\t\t\t\t\n");

            ResetColor();

            // SUPER MARIO THEME! 
            Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);

            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\n\n Aperte ENTER para voltar para o Menu Principal! ");
            ReadKey();
        }
    }
}
