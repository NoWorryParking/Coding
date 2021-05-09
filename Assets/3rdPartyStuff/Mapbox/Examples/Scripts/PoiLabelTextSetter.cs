 namespace Mapbox.Examples
{
	using Mapbox.Unity.MeshGeneration.Interfaces;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class PoiLabelTextSetter : MonoBehaviour, IFeaturePropertySettable
	{
		[SerializeField]
		Text _text;
		[SerializeField]
		Image _background;
        public Dictionary<string, object> info;

		public void Set(Dictionary<string, object> props)
		{
			_text.text = "";
            info = props;
			if (props.ContainsKey("name"))
			{
				_text.text = props["name"].ToString();
                print(_text.text);
			}
			else if (props.ContainsKey("house_num"))
			{
				_text.text = props["house_num"].ToString();
			}
			else if (props.ContainsKey("type"))
			{
				_text.text = props["type"].ToString();
			}
            _text.text = "";
            RefreshBackground();
		}

		public void RefreshBackground()
		{
			RectTransform backgroundRect = _background.GetComponent<RectTransform>();
			LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundRect);
		}
	}
}