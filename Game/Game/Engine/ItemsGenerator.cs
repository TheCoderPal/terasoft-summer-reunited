using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Game.Engine
{
    class ItemsGenerator
    {
       private string[] goodItems = { "banana", "apple", "orange" };
       private string[] badItems = { "hamburg", "fries", "hotdog" };
       private string[] viruses = { "level1", "level2", "level3" };
       private string[] weapons = { "sheild", "sword" };
       private Random random;
        static int  counter;
		public ItemsGenerator()
		{
            counter = 0;
            random = new Random();
		}
        public string[,] generateMore()
        {
            string[] ItemsList = new string[10];
            if (counter == 0)
            {
                ItemsList[0]=weapons[0];
                for (int i = 1; i < 10; i++)
                {
                    if (i == 1 || i == 2 || i == 5)
                        ItemsList[i] = getRandomItem(goodItems);
                    else if (i == 3 || i == 6 || i == 7)
                        ItemsList[i] = "Empty";
                    else
                        ItemsList[i] = getRandomItem(goodItems, badItems);
                }
            }
            else if (counter == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0 || i == 2 || i == 6)
                        ItemsList[i] = "Empty";
                    else if (i == 1 || i == 9)
                    {
                        ItemsList[i] = getRandomItem(new string[] { "level1" }, goodItems);
                    }
                    else if (i == 3)
                    {
                        ItemsList[i] = getRandomItem(badItems, weapons);
                    }
                    else if (i == 5)
                    {
                        ItemsList[i] = getRandomItem(new string[] { "level1" }, badItems);
                    }
                    else if (i == 4 || i == 7 || i == 8)
                    {
                        ItemsList[i] = getRandomItem(goodItems);
                    }
                }
            }
            else if (counter == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0)
                        ItemsList[i] = "Empty";
                    else if (i == 3)
                    {
                        ItemsList[i] = getRandomItem(badItems, goodItems);
                    }
                    else if (i == 4)
                    {
                        ItemsList[i] = "level1";
                    }
                    else if (i == 1 || i == 7)
                    {
                        ItemsList[i] = getRandomItem(new string[] { "level1" }, badItems);
                    }
                    else if (i == 5 || i == 6 || i == 8)
                    {
                        ItemsList[i] = getRandomItem(goodItems);
                    }
                    else if (i == 2)
                    {
                        ItemsList[i] = getRandomItem(new string[] { "Empty" }, weapons);
                    }
                    else if (i == 9)
                    {
                        ItemsList[i] = getRandomItem(weapons);
                    }              
                }
            }
            else if (counter < 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < 3)
                        ItemsList[i] = getRandomItem(goodItems);
                    else if (i < 6)
                        ItemsList[i] = getRandomItem(new string[] { "level1" }, badItems);
                    else if (i < 8)
                        ItemsList[i] = "Empty";
                    else if (i < 9)
                        ItemsList[i] = getRandomItem(goodItems, badItems, new string[] { "level1" });
                    else
                        ItemsList[i] = getRandomItem(goodItems, weapons);
                }
                ItemsList = Randomize(ItemsList);
            }
            else if (counter < 30)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < 3)
                        ItemsList[i] = getRandomItem(goodItems);
                    else if (i < 6)
                        ItemsList[i] = getRandomItem(new string[] { "level2" }, badItems);
                    else if (i < 8)
                        ItemsList[i] = "Empty";
                    else if (i < 9)
                        ItemsList[i] = getRandomItem(badItems, new string[] { "level2" });
                    else
                        ItemsList[i] = getRandomItem(goodItems, weapons);

                }
                ItemsList = Randomize(ItemsList);

            }
            else if (counter < 60)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < 3)
                        ItemsList[i] = getRandomItem(goodItems);
                    else if (i < 8)
                        ItemsList[i] = getRandomItem(new string[] { "level2" }, badItems);
                    else if (i < 9)
                        ItemsList[i] = getRandomItem(goodItems,new string[] { "Empty" });
                    else
                        ItemsList[i] = getRandomItem(weapons, new string[] { "Empty" });
                }
                ItemsList = Randomize(ItemsList);
            }

            else if (counter >= 60)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < 3)
                        ItemsList[i] = getRandomItem(goodItems);
                    else if (i < 8)
                        ItemsList[i] = getRandomItem(new string[] { "level3" }, badItems);
                    else if (i < 9)
                        ItemsList[i] = getRandomItem(goodItems, new string[] { "Empty" });
                    else
                        ItemsList[i] = getRandomItem(weapons);
                }
                ItemsList = Randomize(ItemsList);
            }
           
            string[,] returnedItems = new string[10,2];
            for (int i = 0; i <10;i++ )
                returnedItems[i, 0] = ItemsList[i];
            for(int i =0;i<10;i++)
                returnedItems[i,1] = getRandom(3)+"";
          
            counter++;

            return returnedItems;
        }
        private int getRandom(int max)
        {
            return random.Next(max);
        }
        private string getRandomItem(string []stringItems)
        {  
            return stringItems[getRandom(stringItems.Length)];
        }
        private string getRandomItem(string[] stringItems,string[] stringItems2)
        {
            int randomNumber = getRandom(stringItems.Length + stringItems2.Length);
            if (randomNumber < stringItems.Length)
              return  stringItems[randomNumber];
            else
               return stringItems2[randomNumber - stringItems.Length];
        }
        private string getRandomItem(string[] stringItems, string[] stringItems2,string[] stringItems3)
        {
            int randomNumber = getRandom(stringItems.Length + stringItems2.Length + stringItems3.Length);
            if (randomNumber < stringItems.Length)
                return stringItems[randomNumber];
            else if (randomNumber < stringItems2.Length+ stringItems.Length)
                return stringItems2[randomNumber - stringItems.Length];
            else return stringItems3[randomNumber - stringItems.Length - stringItems2.Length];
        }
        private string[] Randomize(string[] input)
        {
            List<string> inputList = input.ToList();
            string[] output = new string[input.Length];
            int i = 0;

            while (inputList.Count > 0)
            {
                int index = random.Next(inputList.Count);
                output[i++] = inputList[index];
                inputList.RemoveAt(index);
            }

         //   output = input;
            for (int g = output.Length- 1; g > 0; g--)
            {
                int n = random.Next(g + 1);
                string temp = output[g];
                output[g] = output[n];
                output[n] = temp;
            }
            return (output);
        }
    }
}
