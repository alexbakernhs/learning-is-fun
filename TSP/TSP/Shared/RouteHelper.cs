namespace TSP
{
    using System.Collections.Generic;
    public class RouteHelper : IRouteHelper
    {
        public List<List<Coordinate>> GenerateAllPossibleRoutes(List<Coordinate> coords)
        {
            List<int> indexes = new List<int>();
            for(int i = 0; i < coords.Count; i++)
            {
                indexes.Add(i);
            }
            
            foreach (var permu in Permutate(seq, seq.Count))
            {
                foreach (var i in permu)
                {
                    Console.Write(i.ToString() + " ");
                }
                Console.WriteLine();
                count++;
            }
        }
        
        public void RotateRight(List<int> sequence, int count)
        {
            int tmp = sequence[count-1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }
        
        public IEnumerable<List<int>> Permutate(List<int> sequence, int count)
        {
            if (count == 1) yield return sequence;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateRight(sequence, count);
                }
            }
        }
    }
}
