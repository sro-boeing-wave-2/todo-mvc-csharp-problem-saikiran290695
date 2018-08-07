using System;
using Xunit;
using NSuperTest;
using ProjectX;
using ProjectX.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using ProjectX.Controllers;
using ProjectX.Services;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace NSuperUnit.Test
{
    public class UnitTest1
    {
        public HttpClient _client;
        private NoteContext _context;

        public UnitTest1() {
            var host = new TestServer(
                new WebHostBuilder()           
                .UseEnvironment("Testing")
                .UseStartup<Startup>()
                );            
            _context = host.Host.Services.GetService(typeof(NoteContext)) as NoteContext;
            _client = host.CreateClient();          
            _context.Note.AddRange(TestNoteProper1);
            _context.Note.AddRange(TestNoteProper2);
            _context.Note.AddRange(TestNoteDelete);
            _context.SaveChanges();
            
        }
        Note TestNoteProper1 = new Note() {
            Title = "Title-1-Deletable",
            Message = "Message-1-Deletable",
            CheckList = new List<CheckList>()
                        {
                            new CheckList(){ Checklist = "checklist-1", IsChecked = true},
                            new CheckList(){ Checklist = "checklist-2", IsChecked = false}
                        },
            Label = new List<Label>()
                        {
                            new Label(){label = "Label-1-Deletable"},
                            new Label(){ label = "Label-2-Deletable"}
                        },
            Pinned = true
        };
        Note TestNoteProper2 = new Note()
        {
            Title = "Title-3-ForGets",
            Message = "Message-3-ForGets",
            CheckList = new List<CheckList>()
                        {
                            new CheckList(){ Checklist = "checklist-3", IsChecked = true},
                            new CheckList(){ Checklist = "checklist-3", IsChecked = false}
                        },
            Label = new List<Label>()
                        {
                            new Label(){label = "Label-3-ForGets"},
                            new Label(){ label = "Label-3-ForGets"}
                        },
            Pinned = true
        };
        Note TestNoteProper = new Note()
        {
                Id=1,
                Title = "Title-1-Updatable",
                Message = "Message-1-Updatable",
                CheckList = new List<CheckList>()
                        {
                            new CheckList(){ Checklist = "checklist-1", IsChecked = true},
                            new CheckList(){ Checklist = "checklist-2", IsChecked = false}
                        },
                Label = new List<Label>()
                        {
                            new Label(){label = "Label-1-Deletable"},
                            new Label(){ label = "Label-2-Updatable"}
                        },
                Pinned = true
            };
        Note TestNotePut = new Note
        {
            Id = 1,
            Title = "Title-1-Deletable",
            Message = "Message-1-Deletable",
            CheckList = new List<CheckList>()
                        {
                            new CheckList(){ Checklist = "checklist-1", IsChecked = true},
                            new CheckList(){ Checklist = "checklist-2", IsChecked = false}
                        },
            Label = new List<Label>()
                        {
                            new Label(){label = "Label-1-Deletable"},
                            new Label(){ label = "Label-2-Deletable"}
                        },
            Pinned = false
        };
        Note TestNotePost = new Note
        {
            Title = "Title-2-Deletable",
            Message = "Message-2-Deletable",
            CheckList = new List<CheckList>()
                        {
                            new CheckList(){ Checklist = "checklist-2-1", IsChecked = true},
                            new CheckList(){ Checklist = "checklist-2-2", IsChecked = false}
                        },
            Label = new List<Label>()
                        {
                            new Label(){label = "Label-2-1-Deletable"},
                            new Label(){ label = "Label-2-2-Deletable"}
                        },
            Pinned = false
        };
        Note TestNoteDelete = new Note()
        {
            Title = "this is deleted title",
            Message = "some text",
            Pinned = false,
            Label = new List<Label>
               {
                   new Label{ label="My First Tag" },
                   new Label{ label = "My second Tag" },
                   new Label{ label = "My third Tag" },
               },
            CheckList = new List<CheckList>
               {
               new CheckList{Checklist="first item" , IsChecked = true},
               new CheckList{Checklist="second item", IsChecked = true},
               new CheckList{Checklist="third item", IsChecked = true},
               }
        };
                      
        [Fact]
        public async void TestGet()
        {
            var response = await _client.GetAsync("/api/Notes");
            var responsestring = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Note>>(responsestring);
            Console.WriteLine(responsenote.ToString());
            Assert.Equal(3,responsenote.Count);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void TestGetById()
        {
            var response = await _client.GetAsync("/api/Notes?Id=1");
            var responsestring = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Note>>(responsestring);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(TestNoteProper1.IsEquals(responsenote[0]));
        }
        [Fact]
        public async void TestGetByTitle()
        {
            var response = await _client.GetAsync("/api/Notes?Title=Title-3-ForGets");
            var responsestring = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Note>>(responsestring);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(TestNoteProper2.IsEquals(responsenote[0]));
        }
        [Fact]
        public async void TestGetByLabel()
        {
            var response = await _client.GetAsync("/api/Notes?Label=Label-3-ForGets");
            var responsestring = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Note>>(responsestring);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(TestNoteProper2.IsEquals(responsenote[0]));
        }
        [Fact]
        public async void TestPost()
        {
            var json = JsonConvert.SerializeObject(TestNotePost);
            var stringcontent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Notes", stringcontent);
            var responsecontent = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<Note>(responsecontent);
            var responsedata = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, responsedata);
            Assert.True(TestNotePost.IsEquals(responsenote));
        }
        [Fact]
        public async void TestPut()
        {
            var json = JsonConvert.SerializeObject(TestNoteProper);
            var stringcontent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Notes/1", stringcontent);
            var responsecontent = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<Note>(responsecontent);
            var responsedata = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, responsedata);
            Assert.True(TestNoteProper.IsEquals(responsenote));
        }
        [Fact]
        public async void TestDelete()
        {
            var response = await _client.DeleteAsync("api/Notes?Title=this is deleted title");
            var responsecode = response.StatusCode;
            Assert.Equal(HttpStatusCode.NoContent, responsecode);
        }
        //IWebHostBuilder 




    }
}
