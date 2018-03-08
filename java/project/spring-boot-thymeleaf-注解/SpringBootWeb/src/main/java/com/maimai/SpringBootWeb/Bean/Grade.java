package com.maimai.SpringBootWeb.Bean;

/**
 * Created by maimai on 2018-01-17.
 */
public class Grade {
    private int id;
    private String gradeNm;
    private int teacherId;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getGradeNm() {
        return gradeNm;
    }

    public void setGradeNm(String gradeNm) {
        this.gradeNm = gradeNm;
    }

    public int getTeacherId() {
        return teacherId;
    }

    public void setTeacherId(int teacherId) {
        this.teacherId = teacherId;
    }
}
