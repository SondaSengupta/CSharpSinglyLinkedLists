using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode firstNode;
        public SinglyLinkedList()
        {
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        }

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            foreach (object i in values)
            {
                string iValue = i.ToString();
                this.AddLast(iValue);
            }
 
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get {
               string element = ElementAt(i);
                return element; 
            }
            set {
                SinglyLinkedListNode node = this.firstNode;
                for (var a = 0; a < i - 1; a++)
                {
                    node = node.Next;
                }
                SinglyLinkedListNode temp = node.Next.Next; 
                SinglyLinkedListNode intermediate = new SinglyLinkedListNode(value);
                node.Next = intermediate;
                intermediate.Next = temp;
               }
        }

        public void AddAfter(string existingValue, string value)
        {


            SinglyLinkedListNode node = this.firstNode;

            while (node.Value != existingValue)
            {
                node = node.Next;
                if (node == null)
                {
                    throw new ArgumentException();
                }
            }

            var temp = node.Next;
            node.Next = new SinglyLinkedListNode(value);
            node.Next.Next = temp;

        }

        public void AddFirst(string value)
        {

           SinglyLinkedListNode oldFirst = this.firstNode;
           SinglyLinkedListNode newFirst =  new SinglyLinkedListNode(value);
           this.firstNode = newFirst;
           newFirst.Next = oldFirst;
        }

        public void AddLast(string value)
        {
            if (firstNode == null)
            {
                firstNode = new SinglyLinkedListNode(value);
            }
            else
            {
                SinglyLinkedListNode node = this.firstNode;
                while (!node.IsLast())
                {
                    node = node.Next;
                }

                node.Next = new SinglyLinkedListNode(value);
            }
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            SinglyLinkedListNode node = this.firstNode;
            int i = 0;
            while (node != null)
            {
                node = node.Next;
                i++;
            }
            return i;
        }

        public string ElementAt(int index)
        {
            SinglyLinkedListNode node = this.firstNode;

            for (int i = 0; i < index; i++)
            {
                node = node.Next;
                 if (node == null)
                  {
                      throw new ArgumentOutOfRangeException();
                 }
            }

            if (node == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return node.Value;
        }

        public string First()
        {
            if (firstNode == null)
            {
                return null;
            }
            else
            {
                return firstNode.Value;
            }
        }

        public int IndexOf(string value)
        {
            SinglyLinkedListNode node = this.firstNode;

            int i = 0;
            if (firstNode == null)
            {
                return -1;
            }
            while (node.Value != value)
            {
                node = node.Next;
                i++;
                if (node == null)
                {
                    return -1;
                }
            }
            return i;
        }

        public bool IsSorted()
        {
            SinglyLinkedListNode node = this.firstNode;
            if (this.firstNode == null || this.firstNode.Next == null || node.Value == node.Next.Value)
            {
                return true;
            }

            else
            {
                while(node.Next != null)
                {
                    if (node > node.Next)
                    {
                        return false;
                    }
                    node = node.Next;
                }
                return true;
            }

        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {

            SinglyLinkedListNode node = this.firstNode;
            if (node == null)
            {
                return null;
            }
            else
            {

                while (!node.IsLast())
                {
                    node = node.Next;
                }
                return node.Value;
            }
        }

        public void Remove(string value)
        {
            
            SinglyLinkedListNode node = this.firstNode;
            if (node.Value == value)
            {
                firstNode = node.Next;
            }

            int counter = this.IndexOf(value);
            if (counter == -1)
            {
                return;
            }
            for (var i = 0; i < counter - 1; i++)
            {
                node = node.Next;
            }
            node.Next = node.Next.Next;

        }

        private void Swap(SinglyLinkedListNode prevPrev, SinglyLinkedListNode prev, SinglyLinkedListNode curr)
        {
            var temp = prev;
            prev.Next = curr.Next;
            curr.Next = temp;
            if (firstNode == temp)
            {
                firstNode = curr;
            }
            else
            {
                prevPrev.Next = curr;
            }
        }

        private SinglyLinkedListNode NodeAt(int index)
        {
            var result = firstNode;
            for (int i = 0; i < index; i++)
            {
                result = result.Next;
            }
            return result;
        }

        public void Sort()
        {
            if (firstNode == null || firstNode.Next == null)
            {
                return;
            }

            for (int i = 0; i < this.Count(); i++)
            {
               SinglyLinkedListNode lowest = NodeAt(i);
                for (int j = i + 1; j < this.Count(); j++)
			{
			 if (lowest > NodeAt(j)){
                 lowest = NodeAt(j);
             }
			}
                if(lowest != NodeAt(i))
                {
                    Swap(NodeAt(i - 1), NodeAt(i), lowest);
                }
                        //either implement a setter so that it swaps values or we have to swap nodes itself.
                
            }
        }

        public string[] ToArray()
        {
            
            
            string[] stringsplitter = new string[] { ",", " ", "}", "{", "\"" };

            string list = ToString();
            string[] words = list.Split(stringsplitter, StringSplitOptions.RemoveEmptyEntries);
            
            return words;
        }


        public override string ToString()
        {
           SinglyLinkedListNode node = this.firstNode;
           StringBuilder sb = new StringBuilder();
           sb.Append("{ ");
           if (node == null)
           {
               sb.Append("}");
               return sb.ToString();
           }
           while (!node.IsLast())
           {
               sb.Append("\"" + node.Value + "\", ");
               node = node.Next;
               
           }
           sb.Append("\"" + node.Value + "\" }");
           return sb.ToString();
        }
    }
}
