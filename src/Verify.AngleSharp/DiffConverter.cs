﻿using System.Text;
using AngleSharp.Diffing.Core;

static class DiffConverter
{
    public static void Append(IDiff diff, StringBuilder builder)
    {
        if (diff is NodeDiff node)
        {
            var received = node.Test;
            var verified = node.Control;
            builder.AppendLine($@" * Node Diff
   Path: {received.Path}
   Received: {received.Node.NodeValue}
   Verified: {verified.Node.NodeValue}");
            return;
        }

        if (diff is AttrDiff attribute)
        {
            var received = attribute.Test;
            var verified = attribute.Control;
            builder.AppendLine($@" * Attribute Diff
   Path: {received.Path}
   Name: {received.Attribute.Name}
   Received: {received.Attribute.Value}
   Verified: {verified.Attribute.Value}");
            return;
        }

        if (diff is UnexpectedAttrDiff unexpectedAttribute)
        {
            var source = unexpectedAttribute.Test;
            builder.AppendLine($@" * Unexpected Attribute
   Path: {source.Path}
   Name: {source.Attribute.Name}
   Value: {source.Attribute.Value}");
            return;
        }

        if (diff is UnexpectedNodeDiff unexpectedNode)
        {
            var source = unexpectedNode.Test;
            builder.AppendLine($@" * Unexpected Node
   Path: {source.Path}
   Name: {source.Node.NodeName}
   Value: {source.Node.NodeValue}");
            return;
        }

        if (diff is MissingAttrDiff missingAttribute)
        {
            var source = missingAttribute.Control;
            builder.AppendLine($@" * Missing Attribute
   Path: {source.Path}
   Name: {source.Attribute.Name}
   Value: {source.Attribute.Value}");
            return;
        }

        if (diff is MissingNodeDiff missingNode)
        {
            var source = missingNode.Control;
            builder.AppendLine($@" * Missing Node
   Path: {source.Path}
   Name: {source.Node.NodeName}
   Value: {source.Node.NodeValue}");
            return;
        }

        throw new($"Unknown diff: {diff.GetType()}");
    }
}