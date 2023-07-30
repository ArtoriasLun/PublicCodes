// using System;
// using System.Threading.Tasks;
// using GameCreator.Runtime.Common;
// using GameCreator.Runtime.VisualScripting;
// using RootMotion.FinalIK;
// using UnityEngine;

// [Version(1, 0, 0)]

// [Dependency("Final IK", 2, 0, 0)]
// [Title("Set LookAtController Target (Final IK)")]
// [Description("Sets the Target for an LookAtController (Final IK)")]

// [Category("Animation Rigging/Final IK/Set LookAtController Target")]

// [Parameter("Controller", "GameObject with a LookAtController component")]
// [Parameter("Target", "New value for target")]

// [Keywords("Rig", "AimIK", "IK", "Set", "FinalIK", "LookAt")]
// [Image(typeof(IconSkeleton), ColorTheme.Type.Yellow)]

// [Serializable]
// public class InstructionFinalIKSetLookAtControllerTarget : Instruction
// {
//     // MEMBERS: -------------------------------------------------------------------------------
//     [SerializeField] private PropertyGetGameObject m_Controller = new PropertyGetGameObject();
//     [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject();

//     // PROPERTIES: ----------------------------------------------------------------------------

//     public override string Title =>
//         $"LookAtController target from {this.m_Controller} to {this.m_Target}";

//     // RUN METHOD: ----------------------------------------------------------------------------
//     protected override Task Run(Args args)
//     {
//         LookAtController controller = this.m_Controller.Get<LookAtController>(args);
//         var target = this.m_Target.Get<Transform>(args);

//         if (controller == null) return DefaultResult;

//         controller.target = target;

//         return DefaultResult;
//     }
// }