using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlexibleGridLayout : LayoutGroup
{
	public enum GridType { uniform, width, height, FixedRows, FixedColumns };

	[Header("Type of Fitting")]
	[SerializeField] GridType typeOfGrid;

	[Header("Grid Integers")]
	[SerializeField] int rows;
	[SerializeField] int columns;

	[Header("Grid Vectors")]
	[SerializeField] Vector2 cellSize;
	[SerializeField] Vector2 spacing;

	[Header("Grid Booleans")]
	[SerializeField] bool fitX;
	[SerializeField] bool fitY;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();

		//calculate rows an columns
		if (typeOfGrid == GridType.width || typeOfGrid == GridType.height)
		{
			float sqrRT = Mathf.Sqrt(transform.childCount);
			rows = Mathf.CeilToInt(sqrRT);
			columns = Mathf.CeilToInt(sqrRT);
		}

		//change types
		if(typeOfGrid == GridType.width || typeOfGrid == GridType.FixedColumns)
			rows = Mathf.CeilToInt(transform.childCount / (float)columns);
		if(typeOfGrid == GridType.height || typeOfGrid == GridType.FixedRows)
			columns = Mathf.CeilToInt(transform.childCount / (float)rows);

		//Get Size of container
		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		//determine size of children
		float cellWidth = parentWidth / (float) columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
		float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

		cellSize.x = fitX ? cellWidth : cellSize.x;
		cellSize.y = fitY ? cellHeight : cellSize.y;

		//Control the indexes
		int columnCount = 0;
		int rowCount = 0;

		for (int i = 0; i < rectChildren.Count; i++)
		{
			rowCount = i / columns;
			columnCount = i % columns;

			var item = rectChildren[i];

			var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
			var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

			SetChildAlongAxis(item, 0, xPos, cellSize.x);
			SetChildAlongAxis(item, 1, yPos, cellSize.y);
		}
	}
	public override void CalculateLayoutInputVertical()
	{

	}

	public override void SetLayoutVertical()
	{

	}

	public override void SetLayoutHorizontal()
	{

	}
}
