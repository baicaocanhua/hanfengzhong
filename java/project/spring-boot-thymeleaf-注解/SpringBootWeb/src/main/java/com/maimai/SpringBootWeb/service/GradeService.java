package com.maimai.SpringBootWeb.service;

import com.maimai.SpringBootWeb.Bean.Grade;
import com.maimai.SpringBootWeb.dao.GradeMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Created by maimai on 2018-01-17.
 */
@Service
public class GradeService {
    @Autowired
    private GradeMapper gradeMapper;

    public List<Grade> getByGradeNm(String name){
        return gradeMapper.getByGradeNm(name);
    }
}
