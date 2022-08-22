using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class TabButtons : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public enum BackButtonType { Uniform, Switch }

	[Header("External Assets")]
    [SerializeField] TabGroup tabGroup;

	[Header("Switching Logic")]
	public BackButtonType typeOfBackButton;

	[Header("Tab Button Data")]
    public Image bg;

	[Header("Back Button")]
	public bool isBackButton;

	void Start()
    {
        //Find the Image and assign
        bg = GetComponent<Image>();

        tabGroup.Subscribe(this);
    }

	public void Select()
	{
		///MusicManager.Instance.Play("Click");
	}

	public void DeSelect()
	{

	}

	#region Event System Throws
	public void OnPointerEnter(PointerEventData eventData)
	{
		tabGroup.OnTabEnter(this);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		tabGroup.OnTabSelected(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tabGroup.OnTabExit(this);
	}
	#endregion
}
