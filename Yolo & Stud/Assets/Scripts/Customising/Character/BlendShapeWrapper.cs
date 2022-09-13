/// <summary>
/// Wraps All The Customsiing values
/// </summary>

public class BlendShapeWrapper 
{
	public int positiveIndex { get; set; }	
	public int negativeIndex { get; set; }	

	public BlendShapeWrapper(int positiveIndex, int negativeIndex)
	{
		this.positiveIndex = positiveIndex;
		this.negativeIndex = negativeIndex;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/