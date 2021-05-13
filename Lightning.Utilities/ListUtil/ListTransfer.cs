using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// ListTransfer
    /// 
    /// May 13, 2021
    /// 
    /// Utility to transfer lists between two types.
    /// </summary>
    /// <typeparam name="T">The type of the list you wish to transfer from.</typeparam>
    /// <typeparam name="T2">The type of the list you wish to transfer to/</typeparam>
    public class ListTransfer<T, T2> where T2 : T 
    {
        public static List<T2> TransferBetweenTypes(List<T> ListTo)
        {

            if (!typeof(T).IsAssignableFrom(typeof(T2)))
            {
                return null;
            }
            else
            {
                List<T2> NewList = new List<T2>();

                foreach (T Item in ListTo)
                {
                    NewList.Add((T2)Item);
                }

                return NewList;
            }


        }
    }
}
