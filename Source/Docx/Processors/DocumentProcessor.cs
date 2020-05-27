﻿using DocumentFormat.OpenXml.Packaging;
using Docx.DataModel;

namespace Docx.Processors
{
    internal class DocumentProcessor
    {
        private readonly EngineConfig _engineConfig;

        public DocumentProcessor(EngineConfig engineConfig)
        {
            _engineConfig = engineConfig;
        }

        public void Process(WordprocessingDocument document, Model documentModel)
        {
            var mainPart = document.MainDocumentPart;
            var imageProcessor = new ImageProcessor(mainPart);
            var compositeElementProcessor = new CompositeElementProcessor(_engineConfig, imageProcessor);

            compositeElementProcessor.Process(mainPart.Document.Body, documentModel);

            foreach (var hp in mainPart.HeaderParts)
            {
                compositeElementProcessor.Process(hp.Header, documentModel);
            }

            foreach (var fp in mainPart.FooterParts)
            {
                compositeElementProcessor.Process(fp.Footer, documentModel);
            }
        }
    }
}
