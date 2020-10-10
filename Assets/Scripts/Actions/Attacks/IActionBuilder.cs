using Assets.Scripts.Actions;
using System;

namespace Assets.Scripts.Units
{
    public interface IActionBuilder
    {
        IAction CreateAction();
    }
}