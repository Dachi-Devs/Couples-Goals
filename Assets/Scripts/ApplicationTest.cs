using System.Collections.Generic;
using System.Numerics;

public class ApplicationTest
{
    public Dictionary<int, List<int>> FindIntersections(List<Shape> shapes)
    {
        //Initialise Dictionary with all shapes added
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

        foreach (Shape s in shapes)
        {
            dict.Add(s.id, new List<int>());
        }


        //Iterate through shapes
        for (int i = 0; i < shapes.Count - 1; i++)
        {
            //Iterate through next shapes to check intersection
            for (int j = i + 1; j < shapes.Count; j++)
            {
                //Get distance between two shapes centres
                float distance = Vector2.Distance(shapes[j].pos, shapes[i].pos);

                //Circle and Circle
                if (true)
                {
                    //Circles intersect if the distance is less than r1 + r2 and greater than r1 - r2
                    if (distance < shapes[i].radius + shapes[j].radius && distance > shapes[i].radius - shapes[j].radius)
                    {
                        dict[shapes[i].id].Add(shapes[j].id);
                        dict[shapes[j].id].Add(shapes[i].id);
                    }
                }

                //Circle and Rectangle
                if (true)
                {

                }

                //Rectangle and Rectangle
                if (true)
                {

                }
            }
        }



        return dict;
    }
}

public class Shape
{
    public Vector2 pos;
    public bool isCircle;
    public float radius;
    public int id;
}
