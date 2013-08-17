﻿using System.Collections.Generic;
using ExCSS.Model.Extensions;

namespace ExCSS.Model.Factories.AtRuleFactories
{
    internal class PageRuleFactory : RuleFactory
    {
        public PageRuleFactory(StyleSheetContext context) : base(context)
        { }

        public override void Parse(IEnumerator<Block> reader)
        {
            var page = new PageRule(Context);

            Context.ActiveRules.Push(page);

            var selector = new SelectorConstructor();

            do
            {
                if (reader.Current.Type == GrammarSegment.CurlyBraceOpen)
                {
                    if (reader.SkipToNextNonWhitespace())
                    {
                        var tokens = reader.LimitToCurrentBlock();
                        tokens.GetEnumerator().AppendDeclarations(page.Style.List);
                        break;
                    }
                }

                selector.PickSelector(reader);
            }
            while (reader.MoveNext());

            page.Selector = selector.Result;
            Context.ActiveRules.Pop();
        }
    }
}
