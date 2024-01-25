using System;
using System.Collections;
using System.Collections.Generic;
class PlayerPath{
    private Panel panel;
    private List<Tuple<int, int>> points;

    public PlayerPath(Panel panel){
        this.panel = panel;
        points = new List<Tuple<int, int>>();
    }

    public bool AddNode(int indexRow, int indexCol){
        if(!panel.IsPointValid(indexRow, indexCol)){
            return false; // The node is not within the bounds of the panel
        }
        if(indexCol % 2 != 0 || indexRow % 2 != 0){
            return false; // The node is not a node
        }
        // Check if the placement is next to the last point in the path
        if(points.Count > 0){
            Tuple<int, int> pointToBeAdded = new Tuple<int, int>(indexRow, indexCol);
            Tuple<int, int> last = points[points.Count - 1];
            if(points.Contains(pointToBeAdded)){
                return false; // The node is already in the path
            }
            else if(last.First == panel.GetEnd().First && last.Second == panel.GetEnd().Second)
            {
                return false; // The path is already complete
            }
            else if(Math.Abs(last.First - indexRow) + Math.Abs(last.Second - indexCol) != 2)
            {
                return false; // The placement is not next to the last point in the path
            }
            // Compute the index of the edge to be added to link the last point in the path to the new point
            Tuple<int, int> edgeToBeAdded = new Tuple<int, int>((last.First + indexRow) / 2, (last.Second + indexCol) / 2);
            // Check if the edge to be added is already in the path
            if(points.Contains(edgeToBeAdded))
            {
                return false; // The edge to be added is already in the path
            }
            // Add the edge and the node to the path
            points.Add(edgeToBeAdded);
            points.Add(pointToBeAdded);
        }
        else{
            // Check if the placement is on the start position
            if(indexRow != panel.GetStart().First || indexCol != panel.GetStart().Second)
            {
                return false; // The placement is not on the start position
            }
            // Add the node to the path
            points.Add(new Tuple<int, int>(indexRow, indexCol));
        }
        return true;
    }

    public void AddNodeList(List<Tuple<int, int>> nodeList){
        foreach(Tuple<int, int> node in nodeList){
            AddNode(node.First, node.Second);
        }
    }

    public void RemoveLastNode(){
        points.RemoveAt(points.Count - 1);
        if(points.Count > 0){
            points.RemoveAt(points.Count - 1);
        }
    }
    public Tuple<int, int> GetLastNode(){
        return points[points.Count - 1];
    }

    public void PrintPath(){
        panel.PrintPath(points);
    }

    public void PrintPanel(){
        panel.PrintPanel();
    }

    public void PrintRegions(){
        panel.PrintRegions(points);
    }

    public void SetPanel(Panel panel){
        this.panel = panel;
    }

    public Panel GetPanel(){
        return panel;
    }

    public List<Tuple<int, int>> GetPoints(){
        // Return a copy of the list of points
        return new List<Tuple<int, int>>(points);
    }

    public static PlayerPath GenerateRandomPath(Panel panel){
        // Create a path
        PlayerPath path = new PlayerPath(panel);
        // Add the start position to the path
        path.AddNode(panel.GetStart().First, panel.GetStart().Second);
        List<Tuple<int, int>> neighbourNodes = panel.GetNeighbourNodes(panel.GetStart().First, panel.GetStart().Second);
        List<Tuple<int, int>> invalidNeighbourNodes = new List<Tuple<int, int>>();
        while(path.GetLastNode().First != panel.GetEnd().First || path.GetLastNode().Second != panel.GetEnd().Second){
            // Add a random neighbour node to the path
            System.Random random = new System.Random();
            int randomIndex = random.Next(neighbourNodes.Count);
            
            while(neighbourNodes.Count > 0 && !path.AddNode(neighbourNodes[randomIndex].First, neighbourNodes[randomIndex].Second)){
                neighbourNodes.RemoveAt(randomIndex);
                randomIndex = random.Next(neighbourNodes.Count);  
            }
            if(neighbourNodes.Count == 0){
                // Debug.Print("No neighbour nodes left, backtrack");
                // Remove the last node and edge from the path, while storing the last node
                Tuple<int, int> lastNode = path.GetLastNode();
                invalidNeighbourNodes.Add(lastNode);
                path.RemoveLastNode();
                // Update the list of neighbour nodes to the new last node, while removing the last node from the list
                neighbourNodes = panel.GetNeighbourNodes(path.GetLastNode().First, path.GetLastNode().Second);
                foreach(Tuple<int, int> invalidNeighbourNode in invalidNeighbourNodes){
                    neighbourNodes.Remove(invalidNeighbourNode);
                }
                if(neighbourNodes.Count == 0){
                    invalidNeighbourNodes = new List<Tuple<int, int>>();
                }
            }
            else{
                // Update the list of neighbour nodes
                neighbourNodes = panel.GetNeighbourNodes(neighbourNodes[randomIndex].First, neighbourNodes[randomIndex].Second);
            }
        }
        return path;
    }

    internal int[] isPathValid()
    {
        int[] result = new int[3] { 0, 0, 0 };

        // get the grid
        IPuzzleSymbol[,] grid = panel.GetGrid();
        // get regions
        List<List<Tuple<int, int>>> regions = panel.GetRegions(points);
        // check if all squares of a region have the same color
        foreach (List<Tuple<int, int>> region in regions)
        {
            int colorId = -1;
            foreach (Tuple<int, int> square in region)
            {
                if (grid[square.First, square.Second] != null && grid[square.First, square.Second].Name == "Square"){
                    if(colorId == -1)
                    {
                        colorId = grid[square.First, square.Second].GetColorId();
                    }
                    else if (colorId != grid[square.First, square.Second].GetColorId())
                    {
                        // Console.WriteLine("colorId: " + colorId + " != " + grid[square.First, square.Second].GetColorId());
                        result[1]++;
                    }
                }
            }
        }
        // check if for all suns of a region, you can pair it with exactly one other symbol of the same color
        foreach (List<Tuple<int, int>> region in regions)
        {
            foreach (Tuple<int, int> sun in region)
            {
                if (grid[sun.First, sun.Second] != null && grid[sun.First, sun.Second].Name == "Sun")
                {
                    int colorId = grid[sun.First, sun.Second].GetColorId();
                    int count = 1;
                    foreach (Tuple<int, int> symbol in region)
                    {
                        if (grid[symbol.First, symbol.Second] != null && (grid[symbol.First, symbol.Second].Name == "Square" || grid[symbol.First, symbol.Second].Name == "Sun"))
                        {
                            if (grid[symbol.First, symbol.Second].GetColorId() == colorId && symbol != sun)
                            {
                                count++;
                            }
                        }
                    }
                    if (count != 2)
                    {
                        // Console.WriteLine("count: " + count + " != 2");
                        result[2]++;
                    }
                }
            }
        }
        // check if all hexagons are on the path
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                Tuple<int, int> point = new Tuple<int, int>(i, j);
                if (grid[point.First, point.Second] != null && grid[point.First, point.Second].Name == "Hexagon")
                {
                    if (!points.Contains(point))
                    {
                        // Console.WriteLine("Hexagon not on path");
                        result[0]++;
                    }
                }
            }
        }
        return result;
    }
}