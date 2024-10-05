using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
	CityMap map;
	[Export] public DrawMap drawMap { get; set; }
	[Export] public SearchAlgorithms searchAlgorithms { get; set; }
	[Export] public SearchRequest searchRequest { get; set; }
	[Export] public Problem problem { get; set; }

	public override void _Ready()
	{
		Globals.main = this;

		map = CityMap.FromFile("C:/Users/arman/Documents/GodotProjects/search-algorithm-representation/tegucigalpa.json");

		drawMap.drawMap(map);

	} 



}
