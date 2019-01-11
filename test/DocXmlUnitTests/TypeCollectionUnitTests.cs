﻿using System;
using System.Collections.Generic;
using System.Text;
using DocXmlUnitTests.TestData.Reflection;
using LoxSmoke.DocXml.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class TypeCollectionUnitTests
    {
        [TestMethod]
        public void CrackType_SimpleValue()
        {
            var tc = new TypeCollection();
            tc.UnwrapType(null, typeof(string));
            tc.UnwrapType(null, typeof(string));
            Assert.AreEqual(1, tc.ReferencedTypes.Count);
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(string)));
        }

        [TestMethod]
        public void CrackType_GenericType()
        {
            var tc = new TypeCollection();
            tc.UnwrapType(null, typeof(List<Tuple<string, List<double[]>>>));
            Assert.AreEqual(4, tc.ReferencedTypes.Count);
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(string)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(double)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(List<>)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(Tuple<,>)));
        }

        [TestMethod]
        public void GetReferenceTypes_OneAssemblyOnly()
        {
            var tc = new TypeCollection();

            var settings = ReflectionSettings.Default;
            settings.ExamineAssemblies = (a) => a.FullName.Contains("DocXml");

            tc.GetReferencedTypes(typeof(TCTestClass), settings);
            Assert.AreEqual(5, tc.ReferencedTypes.Count);
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum2)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestListPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum1)));
        }

        [TestMethod]
        public void GetReferenceTypes_DefaultSettings()
        {
            var tc = new TypeCollection();
            
            tc.GetReferencedTypes(typeof(TCTestClass));
            Assert.IsTrue(tc.ReferencedTypes.Count > 3);
        }

    }
}
