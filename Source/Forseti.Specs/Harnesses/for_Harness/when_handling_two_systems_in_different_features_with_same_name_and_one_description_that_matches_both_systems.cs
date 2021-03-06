﻿using System.Linq;
using Forseti.Harnesses;
using Machine.Specifications;
using Forseti.Files;
using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti.Specs.Harnesses.for_Harness
{
    [Subject(typeof(Harness))]
    public class when_handling_two_systems_in_different_features_with_same_name_and_one_description_that_matches_both_systems
    {
		const string first_feature_name = "FirstFeature";
		const string second_feature_name = "SecondFeature";
        const string script_path = "Scripts";
        const string system_name = "something";
        const string description_name = "when_the_moon_hits_the_sky";
		
		const string first_system_file = script_path+"/"+first_feature_name+"/"+system_name+".js";
		const string second_system_file = script_path+"/"+second_feature_name+"/"+system_name+".js";

        static Harness harness;
        static IFile[] files;
        static IEnumerable<Suite> affected_suites;


        Establish context = () =>
        {
            harness = new Harness(null)
            {
				SystemsSearchPath = "Scripts/{feature}/{system}.js",
				DescriptionsSearchPath = "Specs/{feature}/for_{system}/{description}.js"
            };

            files = new[] {
                ((File)(first_system_file)),
                ((File)(second_system_file)),
                ((File)("Specs/"+second_feature_name+"/for_"+system_name+"/"+description_name+".js")),
            };
        };

        Because of = () => affected_suites =  harness.HandleFiles(files);

        It should_add_a_suite_with_the_first_system = () => harness.Suites.First().SystemFile.FullPath.ShouldEqual(first_system_file);
        It should_add_a_suite_with_the_second_system = () => harness.Suites.Last().SystemFile.FullPath.ShouldEqual(second_system_file);
        It should_contain_a_description_for_only_second_system = () => 
                                                                    {
                                                                        var firstSystem = harness.Suites.First();
                                                                        var secondSystem = harness.Suites.Last();

                                                                        firstSystem.Descriptions.ShouldBeEmpty();
                                                                        secondSystem.Descriptions.ShouldNotBeEmpty();

                                                                    };

        It should_only_mark_second_systm_as_affected = () => affected_suites.First().SystemFile.FullPath.ShouldEqual(second_system_file);

    }
}
