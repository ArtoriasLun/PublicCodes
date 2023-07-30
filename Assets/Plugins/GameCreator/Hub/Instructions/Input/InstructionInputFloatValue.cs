using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 3)]

    [Title("Input Float to Variable")]
    [Description("Gets the value of a float based input and assigns it to a Variable. " +
        "Useful for using with L2 and R2 Gamepad triggers. An example of how to create an float based input for GC2 can be found here: https://github.com/damvcoool/GameCreator2Samples")]

    [Parameter("Input Float", "Get the Float input from a value.")]
    [Parameter("Store", "Number Variable where the value of the input will be stored.")]
    [Image(typeof(IconButton), ColorTheme.Type.Teal, typeof(OverlayBar))]

    [Category("Input/Input Float to Variable")]
    [Serializable]
    public class InstructionInputFloatValue : Instruction
    {

        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private InputPropertyValueFloat d_InputFloat;
        [SerializeField] private PropertySetNumber d_Store;

        // PROPERTIES: ----------------------------------------------------------------------------
        public override string Title => $"Store input value in {this.d_Store}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            if(!d_InputFloat.IsEnabled)
            {
                d_InputFloat.Enable();
            }
            d_Store.Set(d_InputFloat.Read(), args);
            return DefaultResult;
        }
    }
}   