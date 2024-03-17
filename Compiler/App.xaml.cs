using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Compiler.compiler.viewModels;
using Compiler.data.lexer.Interface;
using Compiler.data.parser;
using Compiler.data.parser.state;
using Compiler.data.service;
using Compiler.domain.entity;
using Compiler.domain.useCases;

namespace Compiler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CompilerViewModel compilerViewModel = new CompilerViewModel(new FileUseCase(new FileService()),
                new TextUseCase(new TextService()), new CompilerUseCase(new CompilerService(new StringLexer(), new Parser())),
                new ReferenceUseCase(new ReferenceService()));
            this.MainWindow = new CompilerWindow(compilerViewModel);
            
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}