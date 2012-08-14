using System;
using System.Collections.Generic;
using System.Linq;
namespace Game.Engine
{
    /// <summary>
    /// Tamer Nabil
    /// </summary>
    class ItemsGenerator
    {
       private string[] goodItems = { "banana", "apple", "orange" };
       private string[] badItems = { "hamburg", "fries", "hotdog" };
       private string[] weapons = { "sheild", "sword" };
       private string[] viruses = { "level1","level2","level3"};
       private string[] level1 = { "level1"};
       private string[] level2 = { "level2"};
       private string[] level3 = { "level3"};
       private string[] itemsList;
       private Random random;
       private int  counter;
		public ItemsGenerator()
		{
            counter = 60;
            random = new Random();
		}
        public string[,] GenerateMore()
        {
            itemsList = new string[10];
            if (counter == 0)
              CounterIsZero();
            else if (counter == 1)
              CounterIsOne();
            else if (counter == 2)
              CounterIsTwo();
            else if (counter < 5)
              CounterLessFive();
            else if (counter < 30)
              CounterLessThirty();
            else if (counter < 60)
              CounterLessSixty();
            else if (counter >= 60)
              CounterEqualOrMoreSixty();

            if(counter%30==0 &&  counter!=0)
                itemsList[5] = "gym";
           
            string[,] returnedItems = new string[10,2];
            for (int i = 0; i <10;i++ )
                returnedItems[i, 0] = itemsList[i];
            for (int i = 0; i < 10; i++)
            {  
                if(returnedItems[i,0].Equals("gym"))
                    returnedItems[i,1] = 3 +"";
                else if (badItems.Contains(returnedItems[i, 0]) && counter >10 && counter < 15) 
                    returnedItems[i, 1] = GetRandom(2) + "";
                else if(viruses.Contains(returnedItems[i, 0])||badItems.Contains(returnedItems[i, 0]) && counter>15)
                    returnedItems[i, 1] = GetRandom(2) + "";
                else
                returnedItems[i, 1] = GetRandom(3) + "";
            }   
            counter++;
            return returnedItems;
        }
        private void CounterIsZero()
        {
                itemsList[0] = weapons[0];
                for (int i = 1; i < 10; i++)
                {
                    if (i == 1 || i == 2 || i == 5)
                        itemsList[i] = GetRandomItem(goodItems);
                    else if (i == 3 || i == 6 || i == 7)
                        itemsList[i] = "Empty";
                    else
                        itemsList[i] = GetRandomItem(Concatenate(goodItems, badItems));
                }
        }
        private void CounterIsOne()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 2 || i == 6)
                    itemsList[i] = "Empty";
                else if (i == 1 || i == 9)
                    itemsList[i] = GetRandomItem(Concatenate(level1, goodItems));
                else if (i == 3)
                    itemsList[i] = GetRandomItem(Concatenate(badItems, weapons));
                else if (i == 5)
                    itemsList[i] = GetRandomItem(Concatenate(level1, badItems));
                else if (i == 4 || i == 7 || i == 8)
                    itemsList[i] = GetRandomItem(goodItems);
            }
        }
        private void CounterIsTwo()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                    itemsList[i] = "gym";
                else if (i == 3)
                    itemsList[i] = GetRandomItem(Concatenate(badItems, goodItems));
                else if (i == 4)
                    itemsList[i] = level1[0];
                else if (i == 1 || i == 7)
                    itemsList[i] = GetRandomItem(Concatenate(level1, badItems));
                else if (i == 5 || i == 6 || i == 8)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i == 2)
                    itemsList[i] = GetRandomItem(Concatenate(new string[] { "Empty" }, weapons));
                else if (i == 9)
                    itemsList[i] = GetRandomItem(weapons);
            }
        }
        private void CounterLessFive()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 3 || i == 6)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i < 6)
                    itemsList[i] = GetRandomItem(Concatenate(level1, badItems));
                else if (i < 9)
                    itemsList[i] = GetRandomItem(Concatenate(badItems, level1));
                else
                    itemsList[i] = GetRandomItem(Concatenate(goodItems, weapons));
            }
            itemsList = Randomize(itemsList);
        }
        private void CounterLessThirty()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 3)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i < 6)
                    itemsList[i] = GetRandomItem(Concatenate(level2, badItems));
                else if (i == 6)
                    itemsList[i] = "Empty";
                else if (i < 9)
                    itemsList[i] = GetRandomItem(Concatenate(level1, level2));
                else
                    itemsList[i] = GetRandomItem(Concatenate(goodItems, weapons));
            }
            itemsList = Randomize(itemsList);
        }
        private void CounterLessSixty()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 3)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i < 8)
                    itemsList[i] = GetRandomItem(Concatenate(level2, badItems));
                else if (i < 9)
                    itemsList[i] = GetRandomItem(Concatenate(goodItems, level1));
                else
                    itemsList[i] = GetRandomItem(Concatenate(weapons, level2));
            }
            itemsList = Randomize(itemsList);
        }
        private void CounterEqualOrMoreSixty()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 2)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i < 8)
                    itemsList[i] = GetRandomItem(Concatenate(level3, badItems));
                else if (i < 9)
                    itemsList[i] = GetRandomItem(Concatenate(badItems, level1, level2));
                else
                    itemsList[i] = GetRandomItem(weapons);
            }
            itemsList = Randomize(itemsList);
        }

        private int GetRandom(int max)
        {
            return random.Next(max);
        }
        private string GetRandomItem(string []stringItems)
        {  
            return stringItems[GetRandom(stringItems.Length)];
        }
        private string [] Concatenate(string[] firstString, string[] secondString, string[] thirdString)
        {
            return Concatenate(firstString,secondString).Concat(thirdString).ToArray();
        }
        private string[] Concatenate(string[] firstString, string[] secondString)
        {
            return firstString.Concat(secondString).ToArray();
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
