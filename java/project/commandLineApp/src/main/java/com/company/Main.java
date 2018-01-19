package com.company;

import com.db.DBUtils;
import com.db.UserMapper;
import com.model.User;
import org.apache.ibatis.session.SqlSession;

public class Main {

    public static void main(String[] args) {
	// write your code here
        SqlSession sqlSession = null;
        try {
            sqlSession = DBUtils.openSqlSession();
            UserMapper userMapper = sqlSession.getMapper(UserMapper.class);
            User user = userMapper.getUser(1l);
            System.out.println(user);
            sqlSession.commit();
        } catch (Exception e) {
            e.printStackTrace();
            sqlSession.rollback();
        } finally {
            if (sqlSession != null) {
                sqlSession.close();
            }
        }
    }
}
