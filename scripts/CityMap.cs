using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public partial class CityMap : Node
{
	List<StringName> cityList = new List<StringName>();
	Dictionary<StringName, Dictionary<StringName, float>> connections = new Dictionary<StringName, Dictionary<StringName, float>>();
	Dictionary<StringName, Vector2> map_positions = new Dictionary<StringName, Vector2>();
	float map_scale;

	public void Initialize(StringName Name, List<StringName> cityList, Dictionary<StringName, 
		Dictionary<StringName, float>> connections, Dictionary<StringName, 
			Vector2> map_positions, float map_scale)
	{
		this.Name = Name;
		this.cityList = cityList;
		this.connections = connections;
		this.map_positions = map_positions;
		this.map_scale = map_scale;

		if (cityList.Count != connections.Count)
		{
			GD.PrintErr("Invalid map: The number of locations does not match the number of origins for connections");
		}
	}

	public StringName getName() { return Name; }
	public List<StringName> getCityList() { return cityList; }
	public Dictionary<StringName, Vector2> getMapPositions() { return map_positions; }
    public List<StringName> getNeighbors(StringName location_name)
	{
		if (connections.ContainsKey(location_name))
		{
			Dictionary<StringName, float> neighbors = connections[location_name];
			List<StringName> neighborList = new List<StringName>(neighbors.Keys);
			return neighborList;
		}
		else
		{
			GD.PrintErr(location_name + " is not in the Map (getNeighbors)");
			return null;
		}
	}
	public float getCost(StringName source, StringName destination)
	{
		if (connections.ContainsKey(source))
		{
			if (connections.ContainsKey(destination))
			{
                Dictionary<StringName, float> neighbors = connections[source];
				float cost = neighbors[destination];
				return cost;
            }
			else
			{
                GD.PrintErr(source + " invalid destination location (getCost)");
                return -1;
			}
		}
		else
		{
			GD.PrintErr(source + " invalid source location (getCost)");
			return -1;
		}
	}

	public float computeStraightLineDistance(StringName source, StringName destination)
	{
		Vector2 src_pos = map_positions[source];
		Vector2 dst_pos = map_positions[destination];

		double raw_distance = Math.Sqrt(Math.Pow(src_pos.X - dst_pos.X, 2) +  Math.Pow(src_pos.Y - dst_pos.Y, 2));
		double scaled_distance = raw_distance / this.map_scale;

		return (float)scaled_distance;
	}

    public static CityMap FromFile(string filename)
    {
        // Read the file
        string jsonString = File.ReadAllText(filename);

        // Parse the JSON data
        var rawData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

        // Extract map properties from the raw data
        string name = rawData["name"].ToString();

        // Deserialize the list of strings and convert to List<StringName>
        var nodeStrings = JsonSerializer.Deserialize<List<string>>(rawData["nodes"].ToString());
        List<StringName> locations = new List<StringName>();
        foreach (var node in nodeStrings)
        {
            locations.Add(new StringName(node));
        }

        // Convert positions from List<float> to Vector2
        Dictionary<StringName, Vector2> mapPositions = new Dictionary<StringName, Vector2>();
        var positionsRaw = JsonSerializer.Deserialize<Dictionary<string, List<float>>>(rawData["positions"].ToString());

        foreach (var position in positionsRaw)
        {
            mapPositions[new StringName(position.Key)] = new Vector2(position.Value[0], position.Value[1]);
        }

        float relScale = float.Parse(rawData["scale"].ToString());

        // Extract connections (edges)
        Dictionary<StringName, Dictionary<StringName, float>> connections =
            new Dictionary<StringName, Dictionary<StringName, float>>();

        // Parse the edges from JSON
        var edges = JsonSerializer.Deserialize<Dictionary<string, List<List<object>>>>(rawData["edges"].ToString());

        foreach (var src in edges)
        {
            var srcName = new StringName(src.Key);
            connections[srcName] = new Dictionary<StringName, float>();

            foreach (var edge in src.Value)
            {
                string dst = edge[0].ToString();
                float cost = float.Parse(edge[1].ToString());
                connections[srcName][new StringName(dst)] = cost;
            }
        }

        // Create and return the CityMap instance
        CityMap cityMap = new CityMap();
        cityMap.Initialize(name, locations, connections, mapPositions, relScale);

        return cityMap;
    }
}
