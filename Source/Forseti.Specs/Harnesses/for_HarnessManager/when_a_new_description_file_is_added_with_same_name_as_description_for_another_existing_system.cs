using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Forseti.Files;
using Forseti.Suites;
using Forseti.Pages;
using Forseti.Harnesses;

namespace Forseti.Specs.Harnesses.for_HarnessManager
{
    public class when_a_new_description_file_is_added_with_same_name_as_description_for_another_existing_system : given.a_harness_manager_with_harness_and_fake_file_system_watcher
    {

        static File description_with_same_name_as_for_another_system;

        Establish context = () =>
            {
                description_with_same_name_as_for_another_system = (File)"Specs/for_nothing/when_doing_stuff.js"; ;

            };

        Because of = () => file_system_watcher.TriggerChange(Forseti.Files.FileChange.Added, description_with_same_name_as_for_another_system);

        It should_mark_all_the_descriptions_in__nothing__suite_for_execution = () => 
                                                        {
                                                            harness_result_used_to_render_page.Cases.ShouldContain(c => c.Description.File == description_for_another_system);
                                                            harness_result_used_to_render_page.Cases.ShouldContain(c => c.Description.File == description_with_same_name_as_for_another_system);
                                                        };
        It should_not_mark_any_other_suites_for_execution = () =>
                                                            {
                                                                harness_result_used_to_render_page.Cases.ShouldEachConformTo(c =>
                                                                    c.Description.Suite == harness_result_used_to_render_page.Suites.FirstOrDefault(s => s.SystemFile == another_system));

                                                                harness_result_used_to_render_page.Cases.Count().ShouldEqual(2);
                                                            };
        It should_add_the_decription_to_the_harness_for_later_runs = () => harness.Suites.First(s => s.SystemFile == another_system).Descriptions.ShouldContain(d => 
                                                                            d.File == description_with_same_name_as_for_another_system);
            

    }

    
}
