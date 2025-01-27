﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

global using System.Diagnostics;
global using System.Collections.Concurrent;
global using System.Reflection;
global using System.Text;
global using System.Text.RegularExpressions;

global using Microsoft.Extensions.DependencyInjection;

global using ProfanityFilter.Services.Extensions;

global using Nito.AsyncEx;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo(
    assemblyName: "ProfanityFilter.Services.Tests")]

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo(
    assemblyName: "ProfanityFilter.Action.Tests")]
