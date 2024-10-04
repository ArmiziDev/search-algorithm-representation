using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
	CityMap map;
	[Export] DrawMap drawMap { get; set; }

	public override void _Ready()
	{
		map = CityMap.FromFile("C:/Users/arman/Documents/GodotProjects/search-algorithm-representation/tegucigalpa.json");

		drawMap.drawMap(map);
	}



}
