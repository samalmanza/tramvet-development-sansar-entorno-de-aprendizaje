
/* este es un simple código para un botón
 hace que un objeto se pueda clickear, y manda una advertencia de que fue clickeado para que otros códigos lo detecten */

using Sansar;
using Sansar.Script;
using Sansar.Simulation;
using System;

namespace ScriptExamples.MyScripts
{
    public class Button : SceneObjectScript
    {
        /* esta es una interacción, sirve para hacer que algo se pueda clickear
         para que el mensaje que los usuarios pueden ver cambie hay que cambiar el DefaultValue
         por ejemplo, podemos poner "Toca aquí" y eso se verá cada que el usuario pase el mouse por el botón
         de todas formas, también se puede cambiar desde Sansar */
        [DefaultValue("Click")]
        public Interaction MyInteraction;
        
        public override void Init()
        {
            /* esto hace que el código se ejecute si se clickea */
            MyInteraction.Subscribe((InteractionData data) =>
            {
                /* esto checa quien lo clickeo, por si hay que mandar mensajes al usuario */
                var agent = ScenePrivate.FindAgent(data.AgentId);

                /* esto checa que sí haya alguien que lo clickeo, solo por si acaso */
                if (agent != null)
                {
                    /* esto manda la advertencia "button_pressed" al mundo
                     para que cualquier otro código que esté en ese mundo lo pueda detectar
                     para que otro código lo pueda detectar solo tiene que buscar el mensaje "button_pressed" específicamente */
                    PostScriptEvent("button_pressed");
                }
            });
        }
    }
}
