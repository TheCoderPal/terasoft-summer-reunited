﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace Game.Engine
{
    /// <summary>
    /// Tamer Nabil
    /// </summary>
    class ItemsGenerator
    {
        #region Tamer Instance Variables + Constructor
       private string[] goodItems = { "tomato", "carrot","strawberry","pineapple","broccoli","orange"};
       private string[] badItems = { "fries", "hamburger","pizza","donut","muffin","hotdog"};
       private string[] weapons = { "sheild", "sword" };
       private string[] viruses = { "level1","level2","level3"};
       private string[] itemsList;
       private Random random;
       private int  counter;
       public ItemsGenerator()
		{
            counter = 0;
            random = new Random();
		}
        #endregion

        #region GenerateMore Method
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
       #endregion
       
        #region Method of filling the list
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
                    itemsList[i] = GetRandomItem(insertItem(viruses[0],goodItems));
                else if (i == 3)
                    itemsList[i] = GetRandomItem(Concatenate(badItems, weapons));
                else if (i == 5)
                    itemsList[i] = GetRandomItem(insertItem(viruses[0], badItems));
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
                    itemsList[i] = viruses[0];
                else if (i == 1 || i == 7)
                    itemsList[i] = GetRandomItem(insertItem(viruses[0], badItems));
                else if (i == 5 || i == 6 || i == 8)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i == 2)
                    itemsList[i] = GetRandomItem(insertItem("Empty",weapons));
                else if (i == 9)
                    itemsList[i] = GetRandomItem(weapons);
            }
        }
        private void CounterLessFive()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 4)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i ==5)
                    itemsList[i] = viruses[0];
                else if (i < 9)
                    itemsList[i] = GetRandomItem(insertItem(viruses[0], badItems));
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
                    itemsList[i] = GetRandomItem(insertItem(viruses[2], badItems));
                else if (i == 6)
                    itemsList[i] = "Empty";
                else if (i < 9)
                   itemsList[i] = GetRandomItem(viruses.Take(2).ToArray());
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
                else if (i < 5)
                    itemsList[i] = viruses[1];
                else if (i < 8)
                    itemsList[i] = GetRandomItem(insertItem(viruses[1], badItems));
                else if (i < 9)
                    itemsList[i] = GetRandomItem(insertItem(viruses[0], goodItems));
                else
                    itemsList[i] = GetRandomItem(insertItem(viruses[2], weapons));
            }
            itemsList = Randomize(itemsList);
        }
        private void CounterEqualOrMoreSixty()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 2)
                    itemsList[i] = GetRandomItem(goodItems);
                else if (i < 5)
                    itemsList[i] = viruses[2];
                else if (i < 8)
                    itemsList[i] = GetRandomItem(insertItem(viruses[2], badItems));
                else if (i < 9)
                    itemsList[i] = GetRandomItem(insertItem(viruses[0],insertItem(viruses[1], badItems)));
                else
                    itemsList[i] = GetRandomItem(weapons);
            }
            itemsList = Randomize(itemsList);
        }
       #endregion
        #region GetRandom Number + Items
        private int GetRandom(int max)
        {
            return random.Next(max);
        }
        private string GetRandomItem(string []stringItems)
        {  
            return stringItems[GetRandom(stringItems.Length)];
        }
        #endregion
        private string[] insertItem(string item,string []itemsInList)
        {
            string[] newList = new string[itemsInList.Length+1];
            itemsInList.CopyTo(newList,0);
            newList[itemsInList.Length] = item;
            return newList;
        }
        #region Method of concatenating arrays
        private string [] Concatenate(string[] firstString, string[] secondString, string[] thirdString)
        {
            return Concatenate(firstString,secondString).Concat(thirdString).ToArray();
        }
        private string[] Concatenate(string[] firstString, string[] secondString)
        {
            return firstString.Concat(secondString).ToArray();
        }
        #endregion
        #region Method of sorting array in random sequence
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
        #endregion
    }
}
