﻿/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace GenDummy.Processors
{
    public class DummyProcessor : IProcessor
    {
        readonly List<IProcessor> _processors;

        public BlockSyntax DummyBlock { get; set; }

        public DummyProcessor()
        {
            _processors = new List<IProcessor>
            {
                new ConstructorProcessor(),
                new DestructorProcessor(),
                new PropertyProcessor(),
                new MethodProcessor(),
                new EventProcessor()
            };

            DummyBlock = SyntaxFactory.Block(statements: SyntaxFactory.ParseStatement("throw new global::System.PlatformNotSupportedException(\"Not Supported Feature\");"));
            foreach (var processor in _processors)
            {
                processor.DummyBlock = DummyBlock;
            }
        }

        public MemberDeclarationSyntax Process(MemberDeclarationSyntax member)
        {
            MemberDeclarationSyntax newMember = null;
            foreach (var processor in _processors)
            {
                newMember = processor.Process(member);
                if (newMember != null)
                {
                    break;
                }
            }
            return newMember;
        }
    }
}
