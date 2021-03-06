﻿#region copyright
// Copyright 2015 Habart Thierry
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;

namespace SimpleIdentityServer.Uma.Core.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public bool IsAuthorizedByRo { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ExpiresIn { get; set; }
        public IEnumerable<TicketLine> Lines { get; set; }
    }
}
