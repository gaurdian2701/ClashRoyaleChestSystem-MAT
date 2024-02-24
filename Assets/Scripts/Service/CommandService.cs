using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandService
{
    public CommandInvoker CommandInvoker { get; private set; }
    public CommandService()
    {
        CommandInvoker = new CommandInvoker();
    }
}
