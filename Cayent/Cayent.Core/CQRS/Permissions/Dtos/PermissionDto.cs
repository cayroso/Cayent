﻿using Cayent.Core.CQRS.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Permissions.Dtos
{
    public class PermissionDto : EntityBaseDto
    {
        public PermissionDto()
        {
            AppId = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }

        public string AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}