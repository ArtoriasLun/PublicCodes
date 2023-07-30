using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]

[Title("Curve Function")]
[Description("Uses a curve as a function to transform one range into another")]

[Category("Math/Arithmetic/Curve Function")]

[Parameter(
		"Input",
		"The value to feed into the function"
	)]
[Parameter(
		"Input Min",
		"Minimum value of the input"
	)]
[Parameter(
		"Input Max",
		"Maximum value of the input"
	)]
[Parameter(
		"Curve",
		"The curve to use as a function"
	)]
[Parameter(
		"Output Min",
		"Minimum value of the output"
	)]
[Parameter(
		"Output Max",
		"Maximum value of the output"
	)]
[Parameter(
		"Output",
		"The result of the input being passed through the function"
	)]

[Keywords("From HLS", "AnimatonCurve", "Func", "Math", "Set", "Lerp", 
	"Interpolate", "Modify", "Convert", "Decimal", "Float", "Clamp")]
[Image(typeof(IconCurveCircle), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]

[Serializable]
public class InstructionCurveFunction : Instruction
{
	// MEMBERS: -------------------------------------------------------------------------------
	[SerializeField]
	private PropertyGetDecimal m_Input = new PropertyGetDecimal();

	[SerializeField]
	private PropertyGetDecimal m_InputMin = new PropertyGetDecimal(0);

	[SerializeField]
	private PropertyGetDecimal m_InputMax = new PropertyGetDecimal(1);

	[SerializeField]
	private AnimationCurve m_Curve = AnimationCurve.Linear(0, 0, 1, 1);

	[SerializeField]
	private PropertyGetDecimal m_OutputMin = new PropertyGetDecimal(0);

	[SerializeField]
	private PropertyGetDecimal m_OutputMax = new PropertyGetDecimal(1);

	[SerializeField]
	private PropertySetNumber m_Output = new PropertySetNumber();

	// PROPERTIES: ----------------------------------------------------------------------------

	public override string Title => string.Format(
		"Curve Function: {0} -> {1}",
		m_Input,
		m_Output
	);

	// RUN METHOD: ----------------------------------------------------------------------------

	protected override Task Run(Args args)
	{
		//Transform input into 0-to-1 range
		float value = Mathf.InverseLerp(
			(float)m_InputMin.Get(args), 
			(float)m_InputMax.Get(args), 
			(float)m_Input.Get(args));

		//Apply curve as a function
		value = m_Curve.Evaluate(value);

		//Transform 0-to-1 range into output
		value = Mathf.Lerp(
			(float)m_OutputMin.Get(args),
			(float)m_OutputMax.Get(args),
			value);

		//Set value and end instruction
		m_Output.Set(value, args);
		return DefaultResult;
	}
}
