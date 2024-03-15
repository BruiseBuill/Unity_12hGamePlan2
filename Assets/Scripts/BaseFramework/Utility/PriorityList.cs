using System.Collections.Generic;

namespace BF.Utility
{
    public class PriorityList<T>
    {
        public struct Node
        {
            public object data;
            public int value;
            public Node(object data, int value)
            {
                this.data = data;
                this.value = value;
            }
        }
        List<Node> list = new List<Node>();

        public void Add(T t, int value)
        {
            list.Add(new Node(t, value));
            Up(list.Count - 1);
        }
        public void Remove()
        {
            if (list.Count < 1)
            {
                return;
            }
            list[0] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            ReBuildHeap(0);
        }
        public object Pop()
        {
            if (list.Count < 1)
            {
                return null;
            }
            return list[0].data;
        }
        void Up(int index)
        {
            if (index > 0 && list[index].value < list[(index - 1) / 2].value)
            {
                Swap(index, (index - 1) / 2);
                Up((index - 1) / 2);
            }
        }
        void ReBuildHeap(int index)
        {
            if (index * 2 >= list.Count - 1)
            {
                return;
            }
            if (index * 2 < list.Count - 2)
            {
                //�����ҽڵ�
                if (list[2 * index + 1].value < list[2 * index + 2].value)
                {
                    if (list[index].value <= list[2 * index + 1].value)
                        return;
                    else
                    {
                        Swap(index, 2 * index + 1);
                        ReBuildHeap(2 * index + 1);
                    }
                }
                else
                {
                    if (list[index].value <= list[2 * index + 2].value)
                        return;
                    else
                    {
                        Swap(index, 2 * index + 2);
                        ReBuildHeap(2 * index + 2);
                    }
                }
            }
            else//index * 2 == list.Count - 2
            {
                if (list[index].value <= list[2 * index + 1].value)
                    return;
                else
                {
                    Swap(index, 2 * index + 1);
                    ReBuildHeap(2 * index + 1);
                }
            }
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Count()
        {
            return list.Count;
        }
        void Swap(int index1, int index2)
        {
            var node = list[index1];
            list[index1] = list[index2];
            list[index2] = node;
        }
    }
}