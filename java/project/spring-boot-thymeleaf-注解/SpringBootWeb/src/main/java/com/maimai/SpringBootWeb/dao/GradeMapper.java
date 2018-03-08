package com.maimai.SpringBootWeb.dao;

import com.maimai.SpringBootWeb.Bean.Grade;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * Created by maimai on 2018-01-17.
 */
public interface GradeMapper {
    @Select("select * from grade where grade_nm=#{name}")
    @Results({
            @Result(column="id", property="id"),
            @Result(column="grade_nm", property="gradeNm"),
            @Result(column="teacher_id", property="teacherId")
    })
    public List<Grade> getByGradeNm(String name);

    @Insert("insert into grade(grade_nm,teacher_id) values(#{gradeNm},#{teacherId})")
    @Options(useGeneratedKeys = true, keyColumn = "id", keyProperty = "id")//设置id自增长
    public void save(Grade grade);
}
