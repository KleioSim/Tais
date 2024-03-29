﻿using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class PopDef : IPopDef
{
    public string PopName { get; init; }

    public bool IsRegisted { get; init; }

    public bool HasFamily { get; init; }

    public IEnumerable<ITaskDef> TaskDefs { get; init; }
}
