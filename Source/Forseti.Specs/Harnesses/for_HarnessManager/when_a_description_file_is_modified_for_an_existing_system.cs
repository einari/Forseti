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
    public class when_a_description_file_is_modified_for_an_existing_system : given.a_harness_manager_with_harness_and_fake_file_system_watcher
    {
        Because of = () => file_system_watcher.TriggerChange(Forseti.Files.FileChange.Modified, description_for_system);

        It should_mark_something_suite_as_affected = () => harness_result.Suites.ShouldContain(s => s.SystemFile == system);
        It should_contain_all_cases_for_the_system = () => 
                                                        {
                                                            harness_result.Cases.ShouldContain(c => c.Description.File == description_for_system);
                                                            harness_result.Cases.ShouldContain(c => c.Description.File == description2_for_system);
                                                        };
        It should_not_contain_cases_for_another_system = () =>
                                                        {
                                                            harness_result.Cases.ShouldEachConformTo(c =>
                                                                c.Description.Suite == harness_result.Suites.FirstOrDefault(s => s.SystemFile == system));

                                                            harness_result.Cases.Count().ShouldEqual(2);
                                                        };

            

    }

    
}
