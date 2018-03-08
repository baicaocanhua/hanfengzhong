package com.maimai.SpringBootWeb;

import com.github.pagehelper.PageHelper;
import com.maimai.SpringBootWeb.Bean.Grade;
import com.maimai.SpringBootWeb.service.GradeService;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.annotation.Resource;
import java.util.List;

/**
 * Created by maimai on 2018-01-17.
 */
@RestController
public class GradeController {
    @Resource
    private GradeService gradeService;

    @RequestMapping("/getByGradeNm")
    public List<Grade> getByGradeNm(String name){
        return gradeService.getByGradeNm(name);
    }

    @RequestMapping("/getByGradeNm2")
    public List<Grade> getByGradeNm2(String name,int pageIndex,int pageSize){
        PageHelper.startPage(pageIndex,pageSize);  //显示第一页2条数据
        return gradeService.getByGradeNm(name);
    }
}
