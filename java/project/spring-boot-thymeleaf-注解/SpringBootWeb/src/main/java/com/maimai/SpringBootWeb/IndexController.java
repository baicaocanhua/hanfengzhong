package com.maimai.SpringBootWeb;

import com.maimai.SpringBootWeb.Bean.Person;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by maimai on 2018-01-17.
 */
@Controller
public class IndexController {



    @RequestMapping("/index")
    public String index2(Model model) {
        Person single = new Person("aa", 11);
        List<Person> people = new ArrayList<>();
        Person p1 = new Person("zhangsan", 11);
        Person p2 = new Person("lisi", 22);
        Person p3 = new Person("wangwu", 33);
        people.add(p1);
        people.add(p2);
        people.add(p3);
        model.addAttribute("singlePerson", single);
        model.addAttribute("people", people);
        return "index";
    }
}
