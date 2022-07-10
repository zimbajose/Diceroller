
namespace Diceroller.lib
{
    //The heap element class
    internal class HeapElement<ObjType>
    {
        private ObjType current_obj;
        private HeapElement<ObjType> next_element = null;

        //Returns the size from it to its childs it is the one called from the heap
        public int size() 
        {
            if (next_element == null) 
            {
                return 1;
            }
            return next_element.size(1);
        }

        //Takes the size of its previous childs and takes the size of its next ones
        protected int size(int s) 
        {
            if (next_element == null)
            {
                return s + 1;
            }
            else 
            {
                return next_element.size(s + 1);
            }
        }

        //Constructor
        public HeapElement(ObjType o) 
        { 
            this.current_obj= o;
        }

        //Returns the object of the heap
        public ObjType getObj() 
        {
            return this.current_obj;
        }
        //Sets the next element of the heap as the sent one
        public void setNext(HeapElement<ObjType> he) 
        {
            this.next_element = he;
        }

        //Returns the next element of the heap
        public HeapElement<ObjType> getNext() 
        {
            return this.next_element;
        }

    }
    
    public class Heap<ObjType>
    {

        //The heap's first element
        private HeapElement<ObjType>? first;
        
        //Adds a object to the heap
        public void push(ObjType obj)
        {
            if (first == null)
            {
                first = new HeapElement<ObjType>(obj);
            }
            else {
                HeapElement<ObjType> temp = new HeapElement<ObjType>(obj);
                temp.setNext(first);
                first = temp;
            }
        }

        //Returns and removes the first element in the list
        public ObjType pop() 
        { 
            ObjType r = first.getObj();
            first = first.getNext();
            return r;
        }
        //Returns the first element in the list
        public ObjType getFirst() 
        {
            return first.getObj();

        }


        //Returns the size of the heap
        public int size() 
        {
            if (first == null) {
                return 0;
            }
            return first.size();
        }



    }
}
