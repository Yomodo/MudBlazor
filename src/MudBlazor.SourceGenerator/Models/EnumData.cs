﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MudBlazor.SourceGenerator.Models;

internal record struct EnumData(string Classname, string Name, string Namespace, string AccessModifier, EnumMember[] Members, bool InconsistentDescriptionAttributeUsage = false);