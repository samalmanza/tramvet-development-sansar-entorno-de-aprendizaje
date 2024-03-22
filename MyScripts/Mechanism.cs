
/* este código mueve un objeto para arriba o para abajo, dependiendo de qué clase estés usando
 la clase GoUp lleva el objeto hacia arriba y a su posición inicial
 la clase GoDown lleva el objeto hacia abajo y a su posición inicial
 This work uses content from the Sansar Knowledge Base. � 2018 Linden Research, Inc. Licensed under the Creative Commons Attribution 4.0 International License (license summary available at https://creativecommons.org/licenses/by/4.0/ and complete license terms available at https://creativecommons.org/licenses/by/4.0/legalcode). */

using Sansar;
using Sansar.Script;
using Sansar.Simulation;
using System;

public class Mechanism : SceneObjectScript
{
    
    public override void Init()
    {
    }


    /* esta clase hace que el objeto se mueva para arriba y regrese a su posición inicial */
    public class GoUp : SceneObjectScript
    {
        /* esta variable indica cuánto se va a mover desde el punto inicial
         esta variable se puede cambiar desde Sansar, para no tener que mover el código */
        [DefaultValue(5.0f)]
        public float Move;

        /* esta variable indica cuánto tiempo se va a tardar en llegar y regresar
         esta variable se puede cambiar desde Sansar, para no tener que mover el código */
        [DefaultValue(5.0f)]
        public double Seconds;
        
        /* esta variable indica cuántas veces se van a repetir las acciones, es decir, cuántas veces va a llegar al
         punto indicado por Move y cuantas veces va a regresar
         esta variable se puede cambiar desde Sansar, para no tener que mover el código */
        [DefaultValue(3)]
        public int Rotations;

        /* esta variable se usa para el while para repetir las acciones, no importa si se cambia el valor porque siempre
         se va a igualar a Rotations al inicio del while y va a terminar en 0 al final */
        public int Around = 0;
        
        
        public override void Init()
        {
            /* checa si el objeto se puede mover
             para que el objeto se pueda mover, hay que convertirlo en un objeto Keyframed
             y activar la opción "Movable from Script" desde Sansar */
            if (ObjectPrivate.IsMovable)
            {
                /* esta línea se usa para escuchar al botón
                 cuando el botón se aprieta, manda el mensaje "button_pressed", el cual esta línea detecta
                 y procede a realizar la acción */
                SubscribeToScriptEvent("button_pressed", (ScriptEventData data) =>
                {
                    /* esta línea se agrega porque Rotations siempre tiene que ser el mismo valor por si se aprieta el
                     botón varias veces, mientras que Around puede cambiar constantemente para que el while funcione */
                    Around = Rotations;
                    
                    /* esto crea el vector con el cual se va a mover */
                    Vector toObject = ObjectPrivate.Position;
                    
                    /* estas tres líneas definen el tamaño del vector creado arriba
                     las dos primeras líneas sirven para que el objeto no se mueva horizontalmente
                     la tercera línea iguala Z a la variable Move para que se mueva verticalmente */
                    toObject.Y = 0.0f;
                    toObject.X = 0.0f;
                    toObject.Z = Move;
                    
                    /* este while repite las acciones de moverse desde el punto inicial y regresar a este */
                    while (Around > 0)
                    {
                        /* vuelve a igualar Z a Move para que se vuelva a mover */
                        toObject.Z = Move;
                        
                        /* mueve el objeto, el primer parámetro registra el punto inicial para sumarlo con el vector toObject
                         el segundo parámetro es la variable Seconds, es decir, el tiempo que va a tardar en moverse
                         el tercer parámetro indica la forma en la que se va a mover, en este caso es Smoothstep */
                        ObjectPrivate.Mover.AddTranslate(ObjectPrivate.Position + toObject, Seconds, MoveMode.Smoothstep);

                        /* esta línea cambia Z a 0, para que regrese al punto inicial
                         después, vuelve a mover el objeto */
                        toObject.Z = 0.0f;
                        ObjectPrivate.Mover.AddTranslate(ObjectPrivate.Position + toObject, Seconds, MoveMode.Smoothstep);
                        
                        /* para el while */
                        Around--;
                    }
                });
            }
        }
    }
    
    /* esta clase hace exactamente lo mismo que la de arriba
     excepto que esta hace que se mueva para abajo en vez de arriba */
    public class GoDown : SceneObjectScript
    {
        [DefaultValue(5.0f)]
        public float Move;

        [DefaultValue(5.0f)]
        public double Seconds;
        
        [DefaultValue(3)]
        public int Rotations;

        public int Around = 0;
        
        public override void Init()
        {
            if (ObjectPrivate.IsMovable)
            {
                SubscribeToScriptEvent("button_pressed", (ScriptEventData data) =>
                {
                    Around = Rotations;
                    
                    Vector toObject = ObjectPrivate.Position;
                        
                    toObject.Y = 0.0f;
                    toObject.X = 0.0f;
                    toObject.Z = Move;
                    
                    while (Around > 0)
                    {
                        toObject.Z = Move;
                        ObjectPrivate.Mover.AddTranslate(ObjectPrivate.Position - toObject, Seconds, MoveMode.Smoothstep);

                        toObject.Z = 0.0f;
                        ObjectPrivate.Mover.AddTranslate(ObjectPrivate.Position - toObject, Seconds, MoveMode.Smoothstep);
                        
                        Around--;
                    }
                });
            }
        }
    }
}
