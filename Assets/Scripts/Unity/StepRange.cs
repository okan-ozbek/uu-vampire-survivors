using UnityEditor;
using UnityEngine;

namespace Unity
{
    public class StepRangeAttribute : PropertyAttribute
    {
        public float Min;
        public float Max;
        public float Step;
        
        public StepRangeAttribute(float min, float max, float step)
        {
            Min = min;
            Max = max;
            Step = step;
        }
    }
    
    [CustomPropertyDrawer(typeof(StepRangeAttribute))]
    public class StepRange : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            StepRangeAttribute stepRange = (StepRangeAttribute)attribute;

            if (property.propertyType == SerializedPropertyType.Float)
            {
                float value = property.floatValue;
                value = Mathf.Round(value / stepRange.Step) * stepRange.Step;
                property.floatValue = EditorGUI.Slider(position, label, value, stepRange.Min, stepRange.Max);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                int value = property.intValue;
                value = Mathf.RoundToInt(value / stepRange.Step) * (int)stepRange.Step;
                property.intValue = EditorGUI.IntSlider(position, label, value, (int)stepRange.Min, (int)stepRange.Max);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use StepRange with float or int.");
            }
        }
    }
}